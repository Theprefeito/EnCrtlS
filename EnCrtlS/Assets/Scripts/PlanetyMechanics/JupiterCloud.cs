using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class JupiterCloud : MonoBehaviour
{
    [SerializeField] GameObject cloud;
    [SerializeField] Transform attackCheck;
    public float ammunition;
    public bool isFacingRight;
    public bool isReset = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {                
        Reset();
    }

    public void MecanicaDeJupiter(InputAction.CallbackContext context)
    {
        if (context.performed && ammunition > 0)
        {
            Instantiate(cloud, attackCheck.position, attackCheck.rotation);
            ammunition--;
        }
    }
    
    private void Reset()
    {
        if (transform.position.y < -6)
        {                       
            ammunition = 0;
        }
    }
}
