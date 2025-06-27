using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Vector2 checkPointPosition;
    [SerializeField] GameObject respawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawn.transform.position = checkPointPosition;
        }    
    }

}
