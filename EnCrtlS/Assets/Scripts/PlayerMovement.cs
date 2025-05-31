using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speedPlayer;
    private Rigidbody2D rigPlayer;
    private SpriteRenderer srPlayer;
    
    
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
    private float normalFallSpeed = 1f; 
    private float fastFallSpeed = 20f;
    
    
    
    
    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.cyan);

        Jump();
              
        if (isDashing)
        {
            return;
        }
       
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        FastFall();
    }

    private void FixedUpdate()
    {
        Move();
        if (isDashing)
        {
            return;
        }
        FastFall();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speedPlayer;

        if (Input.GetAxis("Horizontal") > 0f)
        {
           
           srPlayer.flipX = false;

        }
       
        if (Input.GetAxis("Horizontal") < 0f)
        {
           
           srPlayer.flipX = true;

        }
    }

    void Jump()
    {
        if (inFloor)
        {
            jumpNumber = 2;
        }
       
        if (Input.GetKeyDown(KeyCode.C) && jumpNumber > 0)
        {            
            if (!isDoubleJump) 
            {
                rigPlayer.AddForce(new Vector2(0f, jumpStrange), ForceMode2D.Impulse);
                isDoubleJump = true;
                jumpNumber--;
            }

            if (isDoubleJump)
            {
                rigPlayer.AddForce(new Vector2(0f, jumpStrange), ForceMode2D.Impulse);
                isDoubleJump = false;
                jumpNumber = 0;
            }
        }

        else if (Input.GetKeyUp(KeyCode.C))
        {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocity.x, rigPlayer.linearVelocity.y * 0.5f);
        }
        
    }


    void FastFall()
    {
      
       
       //Agora funciona normal
        
        if (rigPlayer.linearVelocity.y < 0 && Input.GetKey(KeyCode.S))
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
    
    
    
    
    
}
