using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float wallSlideSpeed;
    [SerializeField] LayerMask groundLayer;
    private PlayerMovement player;
    private Rigidbody2D rigPlayer;
    public bool isFacingRight;
    public bool onWall;
    public bool wallSlide;

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
