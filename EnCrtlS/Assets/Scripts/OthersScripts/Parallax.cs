using UnityEngine;
[System.Serializable]



public class ParallaxBackground : MonoBehaviour

{  
    
    [SerializeField] Transform cam;
    [SerializeField] private float efectParallax = 0.2f;
    
    private float spriteWidth;
    private float spriteWidth2;
    private Vector3 lastCamPos;
    
    private Transform [] Backgrounds = new Transform [2]; // = Quantidade de fundos completos 


    void Start()
    {
        if (transform.childCount != 2)
        {
            Debug.LogError($"ParallaxBackground works with 2 background children, Fix '{name}' GameObject");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            Backgrounds[i] = transform.GetChild(i);
        }

        if (cam == null) cam = Camera.main.transform;
        lastCamPos = cam.position;
        
        
        spriteWidth = Backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        spriteWidth2 = Backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void LateUpdate()
    {
        
        Vector3 FollowMovent = cam.position - lastCamPos;
        
        transform.position += new Vector3 (FollowMovent.x * efectParallax, 0, 0);
        transform.position += new Vector3 (0, FollowMovent.y * efectParallax, 0);
        lastCamPos = cam.position;

        foreach (var background in Backgrounds)
        {
            float camDistance = cam.position.x - background.position.x;
            if (Mathf.Abs(camDistance) >= spriteWidth)
            {
                float offset = (camDistance > 0) ? spriteWidth * 2f: -spriteWidth * 2f;
                background.position += new Vector3(offset, 0, 0);
            }

            float camDistance2 = cam.position.y - background.position.y;
            if (Mathf.Abs(camDistance2) >= spriteWidth2)s
            {
                float offset2 = (camDistance2 > 0) ? spriteWidth2 * 2f: -spriteWidth2 * 2f;
                background.position += new Vector3(0, offset2, 0);
            }


        }
        
        
        
    }
    
    
    
    
    
}