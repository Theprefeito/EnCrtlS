using UnityEngine;

public class CloudDead : MonoBehaviour
{
    [SerializeField] float timeToDie;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }

    private void Dead()
    {
        Destroy(gameObject, timeToDie);
    }
}
