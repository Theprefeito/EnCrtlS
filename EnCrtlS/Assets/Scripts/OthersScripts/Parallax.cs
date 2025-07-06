using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float StartPos, lenght;
    public GameObject cam;
    public float parallaxEffect;
    
    
    
    
    
    
    
    
    
    void Start()
    {
        StartPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;


        float movement = cam.transform.position.x * (1 - parallaxEffect);
        
        transform.position = new Vector3(StartPos + distance, transform.position.y, transform.position.z);


        if (movement > StartPos + lenght )
        {
            StartPos += lenght;
        }
        else if (movement < StartPos - lenght)
        {
            StartPos -= lenght;
        }
    }
}
