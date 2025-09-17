using System.Collections;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{

   

    public float Timetofall = 0.3f;
    public float Timetodestroy = 1.5f;

    [SerializeField] Transform player;


    public bool Fall = false;
    
    [SerializeField] private Rigidbody2D rb;


    private Vector3 initPos;



    private IEnumerator FallthePlatform()
    {
        yield return new WaitForSeconds(Timetofall);
        rb.bodyType = RigidbodyType2D.Dynamic;
       
      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallthePlatform());
            Fall = true;
        }
        else Fall = false;
        {
            StartCoroutine(ResetAfterTouch());
        }
      
       
        
    }
      
    
    void Start()
    {
        initPos = transform.position;
    }

    
    void Update()
    {
      
        if (player.transform.position.y < -6)
        {
            if (gameObject.activeSelf)
            {
                ResetAfterDeath();
                gameObject.SetActive(true);
            }
        }


       
        
    }   
    
     public void ResetAfterDeath()
     {
             gameObject.SetActive(true);
             transform.position = initPos;
             rb.bodyType = RigidbodyType2D.Static;
     }
    
    private IEnumerator ResetAfterTouch()
    {
        yield return new WaitForSeconds(8);
        gameObject.SetActive(true);
        transform.position = initPos;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
