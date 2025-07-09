using UnityEngine;

public class TrasitionScript : MonoBehaviour
{
    private Animator animTransition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animTransition = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void isOpen()
    {
        animTransition.SetTrigger("isOpen");
    }
}
