using UnityEngine;

public class BoxDIalogue : MonoBehaviour
{
    
    public GameObject box;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            box.SetActive(true);
        }
    }
}
