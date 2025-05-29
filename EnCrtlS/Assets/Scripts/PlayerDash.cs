using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
   
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 12f;
    public float dashTime = 0.2f;
    private float dashCooldowm = 1f;
   
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    [SerializeField]private TrailRenderer tr;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
           StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldowm);
        canDash = true;
    }
    
    
      
    

    
    
    
    
    
}

