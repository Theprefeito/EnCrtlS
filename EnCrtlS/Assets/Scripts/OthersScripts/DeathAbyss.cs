using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAbyss : MonoBehaviour
{
    public float limitsX;
    public float limitsY;
    [SerializeField] GameObject respawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Limits();
    }

    void Limits()
    {
        if (transform.position.x < limitsX)
        {
            transform.position = new Vector3(limitsX, transform.position.y, transform.position.z);
        }
        
        if (transform.position.y < limitsY)
        {
            transform.position = respawnPoint.transform.position;
        }
    }
}
