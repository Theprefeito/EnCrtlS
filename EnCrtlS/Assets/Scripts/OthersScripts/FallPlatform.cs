using System.Collections;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{

   

    public float Timetofall = 0.3f;
    public int Timetodestroy = 6;

    [SerializeField] Transform player;


    public bool Fall = false;
    
    [SerializeField] private Rigidbody2D rb;


    private Vector3 initPos;

    private Renderer Fallrend;

    private IEnumerator FallthePlatform()
    {
        yield return new WaitForSeconds(Timetofall);
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (!Fallrend.isVisible)
        {
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(8);
        gameObject.SetActive(true);
        transform.position = initPos;
        rb.bodyType = RigidbodyType2D.Static;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallthePlatform());
            Fall = true;
        }
      
       
        
    }


    void Start()
    {
        initPos = transform.position;
        Fallrend = GetComponent<Renderer>();
    }

    
    void Update()
    {
      
        if (player.transform.position.y < -6)
        {
            if (gameObject.activeSelf)
            {
                ResetAfterDeath();
              
            }
        }


       
        
    }   
    
     public void ResetAfterDeath()
     {
             gameObject.SetActive(true);
             transform.position = initPos;
             rb.bodyType = RigidbodyType2D.Static;
     }
    
   
}
