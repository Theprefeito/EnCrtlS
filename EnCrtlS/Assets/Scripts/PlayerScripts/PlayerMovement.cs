using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speedPlayer;
    private Rigidbody2D rigPlayer;
    private SpriteRenderer srPlayer;
    private Animator animPlayer;
    
    [Header("Jump")]
    [SerializeField] float jumpStrange;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int jumpNumber;
    public bool inFloor;
    public bool isDoubleJump;

    [Header("Dash")]
    [SerializeField] private TrailRenderer tr;
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 12f;
    public float dashTime = 0.2f;
    private float dashCooldowm = 1f;
   
    
    
    [Header("Faster Fall")]
    private float normalFallSpeed = 2.5f; 
    private float fastFallSpeed = 4f;
    //Criar uma váriavel para controlar velocidade máxima de queda do player, para não bugar no chão por causa de velocidades extremas

    [Header("Wall Slide")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float wallSlideSpeed;
    public bool isFacingRight;
    public bool onWall;
    public bool wallSlide;

    [Header("Wall Jump")]    
    [SerializeField] float wallJumpingDirection;
    [SerializeField] float wallJumpingTime = 0.2f;
    [SerializeField] float wallJumpingCounter;
    [SerializeField] float wallJumpingDuration = 0.4f;
    [SerializeField] Vector2 wallJumpingPower;
    public bool flipado;
    private bool isWallJumping;

    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
        animPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer); //aqui define quando o player est� no ch�o
        Debug.DrawLine(transform.position, groundCheck.position, Color.cyan); // aqui desenha uma linha no debug apenas

        Jump(); //esse � o void Jump
              
        if (isDashing)
        {
            return;
        }
       
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (!isWallJumping)
        {
            Flip();
        }

        /*
        if (!isWallJumping)
        {
           
        }
        */

        FastFall();
    }

    private void FixedUpdate()
    {
        Move();
        FastFall();
        CheckWallNextTo();
        CheckWallSlide();
        WallJump();
        

        if (isDashing)
        {
            return;
        }
    
    }

    void Move()
    {
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speedPlayer;

        float speedForAnimations = Input.GetAxis("Horizontal"); //é usado apenas usado neste caso
        animPlayer.SetFloat("Speed", math.abs(speedForAnimations));

        if (Input.GetAxis("Horizontal") > 0f)
        {
           
           srPlayer.flipX = false; //Flipa o player para a direita
            flipado = false;
            wallJumpingDirection = -1f;
        }
       
        if (Input.GetAxis("Horizontal") < 0f)
        {
           
           srPlayer.flipX = true; //Flipa o player para a esquerda
            flipado = true;
            wallJumpingDirection = 1f;
        }
    }

    void Jump()
    {
        /*
        if (inFloor)
        {
            jumpNumber = 2;
        }
       */

        
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            rigPlayer.AddForce(new Vector2(0f, jumpStrange), ForceMode2D.Impulse);
            isDoubleJump = true;
            jumpNumber--;
        }



        else if (Input.GetButtonUp("Jump"))
        {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocity.x, rigPlayer.linearVelocity.y * 0.5f);
        }
        
    }


    void FastFall()
    {
      
       
       //Agora funciona normal
        
        if (rigPlayer.linearVelocity.y < 0 && Input.GetAxis("Vertical") < 0f)
        {
          rigPlayer.linearVelocity += Vector2.up * Physics2D.gravity.y * (fastFallSpeed - 1) * Time.deltaTime;
        }
        
       
        
    }
    
    
    
    
    
     private IEnumerator Dash()
     {
        
        canDash = false;
        isDashing = true;

        float directionDash = srPlayer.flipX ? -1f : 1f;
        
        
        float originalGravity = rigPlayer.gravityScale;
        rigPlayer.gravityScale = 0f;
        rigPlayer.linearVelocity = Vector2.zero;
        rigPlayer.linearVelocity = new Vector2 (directionDash * dashPower, 0f);
        tr.emitting = true;
        
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rigPlayer.linearVelocity = Vector2.zero;
        
        rigPlayer.gravityScale = originalGravity;
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldowm);
        canDash = true;
    
     }


    void CheckWallNextTo()
    {
        onWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
    }

    private void CheckWallSlide()
    {
        if (onWall && inFloor == false && rigPlayer.linearVelocity.y < 0 && Input.GetAxis("Horizontal") != 0f)
        {
            wallSlide = true;
        }

        else
        {
            wallSlide = false;
        }

        if (wallSlide)
        {
            if (rigPlayer.linearVelocity.y < -wallSlideSpeed)
            {
                rigPlayer.linearVelocityY = -wallSlideSpeed * Time.deltaTime;
            }

        }
    }

    void Flip()
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

    private void WallJump()
    {
        if (inFloor)
        {
            wallJumpingCounter = 0f;
        }
        
        if (wallSlide)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Jump") && wallJumpingCounter > 0f && !inFloor)
        {
            isWallJumping = true;
            rigPlayer.linearVelocity = Vector2.zero;
            rigPlayer.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                flipado = !flipado;
                if (flipado)
                {
                    srPlayer.flipX = true;
                }
            
                else if (!flipado)
                {
                    srPlayer.flipX = false;
                }
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
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

}
