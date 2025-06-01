using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float forceTrampoline;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceTrampoline, ForceMode2D.Impulse);
        }
    }
}
