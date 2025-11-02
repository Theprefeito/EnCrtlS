using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject objetoComTag = GameObject.FindGameObjectWithTag("Cloud");
       
       

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objetoComTag = GameObject.FindGameObjectWithTag("Cloud");

        if (collision.gameObject.CompareTag("Spike") && objetoComTag != null)
        {
            objetoComTag.gameObject.SetActive(false);
        }
    }
}
