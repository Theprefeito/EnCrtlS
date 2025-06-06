using UnityEngine;

public class WallJump : MonoBehaviour
{
    [Header("Wall Slide")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float wallSlideSpeed;
    public bool isFacingRight;
    public bool onWall;
    public bool wallSlide;

    [Header("Wall Jump")]
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);


    [Header("Ground Layer")]
    [SerializeField] LayerMask groundLayer;        
    private PlayerMovement player;
    private Rigidbody2D rigPlayer;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWallNextTo();
        CheckWallSlide();
        Flip();
    }

    void CheckWallNextTo()
    {
        onWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
    }

    private void CheckWallSlide()
    {
        if (onWall && player.inFloor == false && rigPlayer.linearVelocity.y < 0 && Input.GetAxis("Horizontal") != 0f)
        {
            wallSlide = true;
        }

        else
        {
            wallSlide = false;
        }
   
        if(wallSlide)
        {
            if(rigPlayer.linearVelocity.y < -wallSlideSpeed)
            {
                rigPlayer.linearVelocityY = -wallSlideSpeed;
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

    private void WallJumpVoid()
    {
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

        if (Input.GetKeyDown(KeyCode.C) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rigPlayer.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;




            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        if (!isFacingRight)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }

        else
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x - wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
        
    }
}
