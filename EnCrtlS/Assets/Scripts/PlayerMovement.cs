using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speedPlayer;
    private Rigidbody2D rigPlayer;

    [Header("Jump")]
    [SerializeField] float jumpStrange;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    public bool inFloor;
    public int jumpNumber;

    [Header("Dash")]
    [SerializeField] private TrailRenderer tr;
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 12f;
    public float dashTime = 0.2f;
    private float dashCooldowm = 1f;
   

    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.cyan);

        if (inFloor) 
        {
            jumpNumber = 1;
        }


        if (Input.GetKeyDown(KeyCode.C) && jumpNumber > 0)
        {
            rigPlayer.AddForce(new Vector2(0f, jumpStrange), ForceMode2D.Impulse);
            jumpNumber--;
        }

        else if (Input.GetKeyUp(KeyCode.C))
        {
            rigPlayer.linearVelocity = new Vector2(rigPlayer.linearVelocity.x, rigPlayer.linearVelocity.y * 0.5f);
        }


        if (isDashing)

        {
            return;
        }
       
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }

    private void FixedUpdate()
    {
        Move();
        if (isDashing)
        {
            return;
        }

    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speedPlayer;

        if (Input.GetAxis("Horizontal") > 0f)
        {
           
            GetComponent<SpriteRenderer>().flipX = false;

        }
       
        if (Input.GetAxis("Horizontal") < 0f)
        {
           
            GetComponent<SpriteRenderer>().flipX = true;

        }
    }

    
    
     private IEnumerator Dash()
     {
        
        canDash = false;
        isDashing = true;
        
        Vector2 directionDash = new Vector2(Input.GetAxis("Horizontal"), 0f);

        if (directionDash == Vector2.zero)
        {
           directionDash = new Vector2(transform.localScale.x, 0f);
        }
        
        
        float originalGravity = rigPlayer.gravityScale;
        rigPlayer.gravityScale = 0f;
        rigPlayer.linearVelocity = Vector2.zero;
        rigPlayer.linearVelocity = (directionDash * dashPower);
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
