using System.Collections;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    
    //Se colocar 0 de tempo cai na hora, dá pra fazer vários tipos dessa plataforma com o mesmo script
    
    
    public float Timetofall = 0.3f;
    public float Timetodestroy = 1.5f;
    
    [SerializeField] private Rigidbody2D rb;






    private IEnumerator FallthePlatform()
    {
        yield return new WaitForSeconds(Timetofall);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, Timetodestroy);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallthePlatform());
        }
    }
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
