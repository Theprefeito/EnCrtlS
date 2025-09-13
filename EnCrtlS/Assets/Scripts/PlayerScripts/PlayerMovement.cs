using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float speedPlayer;
    private Rigidbody2D rigPlayer;
    private SpriteRenderer srPlayer;
    private Animator animPlayer;
    private bool canMove = true;

    [Header("Jump")]
    [SerializeField] float jumpStrange;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int jumpNumber;
    
    public bool isDoubleJump;

    [Header("Dash")]
    [SerializeField] private TrailRenderer tr;
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 12f;
    public float dashTime = 0.2f;
    private float dashCooldowm = 1f;
   
    
    
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

    
    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
        animPlayer = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.1f, 0.3f), 0, groundLayer);

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
       
        Jump(); //esse � o void Jump
        
        if (isDashing)
        {
            return;
        }
       
        if (Input.GetButtonDown("Fire2") && canDash)
        {
            StartCoroutine(Dash());
        }
        
        WallSlide();
        CairMaisRApido();
        inFloor();
      
    }

    private void FixedUpdate()
    {
        if (canMove)
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
   
    void Move()
    {
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); //Variavel que define a direção que vc está indo
        transform.position += movement * Time.deltaTime * speedPlayer; //Serve para mover o Player

        if (Input.GetAxis("Horizontal") > 0f)
        {
           
           srPlayer.flipX = false; //Flipa o player para a direita
            
        }
       
        if (Input.GetAxis("Horizontal") < 0f)
        {
           
           srPlayer.flipX = true; //Flipa o player para a esquerda
            
        }
    }

    void Jump()
    {



        if (Input.GetButtonDown("Fire1") && (coyoteCounter > 0f || isSliding)) //Esse metodo define que é possivel pular
        {
            rigPlayer.AddForce(new Vector2(0f, jumpStrange), ForceMode2D.Impulse);

        }



        else if (Input.GetButtonUp("Fire1") && rigPlayer.linearVelocityY > 0f) //Funcao do pulo variavel
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
        if(isWallTouch && Input.GetAxis("Horizontal") != 0f) 
        {
            isSliding = true;
        }

        else
        {
            isSliding = false;  
        }

        if (isSliding)
        {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocity.x,Mathf.Clamp(rigPlayer.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    

    }


    private IEnumerator Dash()
     {
        
        canDash = false;
        isDashing = true;

        float directionDash = srPlayer.flipX ? -1f : 1f; //Rever essa linha para colocar oq significa 
        
        
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
        if (Input.GetAxis("Horizontal") > 0f)
        {
            if (isFacingRight)
            {
                Vector3 attackPos = wallCheck.localPosition;
                attackPos.x *= -1;
                wallCheck.localPosition = attackPos;

             
                isFacingRight = false;
            }
        }

        if (Input.GetAxis("Horizontal") < 0f)
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
        float speedForAnimations = Input.GetAxis("Horizontal"); //é usado apenas usado neste caso
        animPlayer.SetFloat("Speed", math.abs(speedForAnimations));
        animPlayer.SetBool("IsFall", false);
        animPlayer.SetBool("isJump", false);
    }


  
}
