using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;
using UnityEditor.Experimental.GraphView;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float speedPlayer;
    private Rigidbody2D rigPlayer;
    private SpriteRenderer srPlayer;
    private Animator animPlayer;
    public bool canMove = true;
    public Vector2 direcao;

    [Header("Jump")]
    [SerializeField] float jumpStrange;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int jumpNumber;
    private bool Canjump;
    
    public bool isDoubleJump;

    [Header("Dash")]
    [SerializeField] private TrailRenderer tr;
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 12f;
    public float dashTime = 0.2f;
    private float dashCooldowm = 1f;

    [Header("Wall Jump")]
    [SerializeField] float wallJumpingDirection;
    [SerializeField] float wallJumpingTime = 0.2f;
    [SerializeField] float wallJumpingDuration = 0.4f;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);
    private float wallJumpingCounter;
    private bool isWallJumping;
    

    [Header("Faster Fall")]
  
    public float maxfallspeed = -30f; // Se tiver bugando uso isso depois

    [Header("Wall Slide")]
    [SerializeField] Transform wallCheck;
    public bool isWallTouch;
    public bool isSliding;
    [SerializeField] float wallSlidingSpeed;
    private bool isFacingRight;


    [Header("Coyote")]   
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float coyoteCounter;
   
    
    [SerializeField] Transform NpcTransform;

    [Header("Audio")]
    public AudioClip dashSound;
    public AudioClip jumpSound;


    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
        animPlayer = GetComponent<Animator>();        
    }

   
    // Update is called once per frame
    void Update()
    {
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.1f, 0.3f), 0, groundLayer); //Detecta a colisão do wallCheck na parede

        if (inFloor())
        {
            coyoteCounter = coyoteTime;
           
            if (!isDashing)
            {
                AnimationWalkPlayer();
            }
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
            
            if (!isDashing)
            {
                AnimationJumpPlayer();
            }
        }
               
        if (isDashing)
        {
            return;
        }
                      
        WallSlide();
        CairMaisRApido();
        inFloor();       
    }


    private void FixedUpdate()
    {
        if (canMove && !isWallJumping)
        {
            Move();
        }
        CairMaisRApido();
        FlipWallCheck();
        AnimationPlayer();

        if (isDashing)
        {
            return;
        }
    
    }

    public bool inFloor()
    {
       return  Physics2D.OverlapCircle(groundCheck.position, 0.2f ,groundLayer); //Serve pra definir se está no chão ou não
    }

    public void analogicMove(InputAction.CallbackContext context)
    {
        direcao = context.ReadValue<Vector2>();
    }
    
    void Move()
    {
        
        Vector3 movement = new Vector3(direcao.x, 0f, 0f); //Variavel que define a direção que vc está indo
        transform.position += movement * Time.deltaTime * speedPlayer; //Serve para mover o Player

        if (direcao.x > 0f)
        {
           
           srPlayer.flipX = false; //Flipa o player para a direita
           wallJumpingDirection = -1f;
        }
       
        if (direcao.x < 0f)
        {
           
           srPlayer.flipX = true; //Flipa o player para a esquerda
           wallJumpingDirection = 1f;
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteCounter > 0f && !isDashing) //Esse metodo define que é possivel pular
        {
           
            rigPlayer.AddForce(new Vector2(0f, jumpStrange), ForceMode2D.Impulse);
            SoundsScript.instance.SoundExecuter(jumpSound);
        }

        else if (context.canceled && rigPlayer.linearVelocityY > 0f) //Funcao do pulo variavel
        {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocity.x, rigPlayer.linearVelocity.y * 0.5f);
            coyoteCounter = 0f;
        }
        
    }


    void CairMaisRApido() //Dá pra a mecanica de planar com isso, -1 já plana 
    {
      
      if(rigPlayer.linearVelocityY < maxfallspeed)
      {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocityX, maxfallspeed); //Isso faz com que ele atinga a velocidade setada na variavel de MaxfallSpeed
      }
               
    }
    
    
    void WallSlide()
    {
        if(isWallTouch && direcao.x != 0f && coyoteCounter < coyoteTime)  //Serve para ele só dar slide se tiver presionando o botão da direção da parede
        {
            isSliding = true;
        }

        else
        {
            isSliding = false;  
        }

        if (isSliding)
        {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocity.x,Mathf.Clamp(rigPlayer.linearVelocity.y, -wallSlidingSpeed, float.MaxValue)); //Diminiu a velocidade de queda quando esta no slide
        }
    

    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (isSliding)
        {
            isWallJumping = false;            
            wallJumpingCounter = wallJumpingTime; //seta o valor do timer = ao valor da duração do Wall jump

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime; //inicia do timer do wall jump
        }

        if (context.performed && wallJumpingCounter > 0f) //quando precionar o botao de pulo inicia o wall jump
        {
            isWallJumping = true;
            rigPlayer.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y); //adiciona a força diagonal do wall jump
            wallJumpingCounter = 0f; //Reseta o timer
            SoundsScript.instance.SoundExecuter(jumpSound);

            StartCoroutine(StopWallJumping()); //cancela o wall jump
        }

                            
    }


    private IEnumerator StopWallJumping()
    {
        yield return new WaitForSeconds(wallJumpingDuration);
        rigPlayer.linearVelocity = new Vector2(0f, rigPlayer.linearVelocity.y * 0.5f);                
        isWallJumping = false; //cancela o wall jump
    }

    public void analogicDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
     {
        
        canDash = false;
        isDashing = true;

        float directionDash = srPlayer.flipX ? -1f : 1f; //Rever essa linha para colocar oq significa 
        SoundsScript.instance.SoundExecuter(dashSound);

        float originalGravity = rigPlayer.gravityScale;
        rigPlayer.gravityScale = 0f;
        rigPlayer.linearVelocity = Vector2.zero;
        rigPlayer.linearVelocity = new Vector2 (directionDash * dashPower, 0f);
        tr.emitting = true;
                                                                                //Dashei
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rigPlayer.linearVelocity = Vector2.zero;
                                                                                //DesDashei
        rigPlayer.gravityScale = originalGravity;
        isDashing = false;

       
                                                                             
        yield return new WaitForSeconds(dashCooldowm);
        canDash = true;
                                                                                //Posso Dashar dnv
     }


    //Essas duas fun��es s�o para manter o Player na Plataforma
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform;
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }

    void FlipWallCheck()
    {
        if (direcao.x > 0f)
        {
            if (isFacingRight)
            {
                Vector3 attackPos = wallCheck.localPosition;
                attackPos.x *= -1;
                wallCheck.localPosition = attackPos;

             
                isFacingRight = false;
            }
        }

        if (direcao.x < 0f)
        {
            if (!isFacingRight)
            {
                Vector3 attackPos = wallCheck.localPosition;
                attackPos.x *= -1;
                wallCheck.localPosition = attackPos;

               
                isFacingRight = true;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Room"))
        {
            Camera.main.GetComponent<CameraFollowV2>().SetCurrentRoom(other.transform); // serve para chamar a função do script CameraFollowV2
        }
    }
    void AnimationPlayer()
    {
        if (isDashing)
        {
            animPlayer.SetBool("Dash", true);
        }
        else
        {
            animPlayer.SetBool("Dash", false);
        }
    }

    void AnimationJumpPlayer()
    {
        float velocityY = rigPlayer.linearVelocity.y;
        if( velocityY > 0) //Pulando
        {
            animPlayer.SetBool("IsFall", false);
            animPlayer.SetBool("isJump", true);
        }
        else if(velocityY < 0) //Caindo
        {
            animPlayer.SetBool("IsFall", true);
            animPlayer.SetBool("isJump", false);
        }
    }

    void AnimationWalkPlayer()
    {
        float speedForAnimations = direcao.x; //é usado apenas usado neste caso
        animPlayer.SetFloat("Speed", math.abs(speedForAnimations));
        animPlayer.SetBool("IsFall", false);
        animPlayer.SetBool("isJump", false);
    }


  
}
