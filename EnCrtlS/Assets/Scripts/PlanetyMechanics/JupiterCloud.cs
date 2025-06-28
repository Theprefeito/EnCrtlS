using NUnit.Framework.Constraints;
using UnityEngine;

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
        MecanicaDeJupiter();
        Flip();
        Reset();
    }

    void MecanicaDeJupiter()
    {
        if (Input.GetButtonDown("Fire3") && ammunition > 0)
        {
            Instantiate(cloud, attackCheck.position, attackCheck.rotation);
            ammunition--;
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0f)
        {
            if (isFacingRight)
            {
                Vector3 attackPos = attackCheck.localPosition;
                attackPos.x *= -1;
                attackCheck.localPosition = attackPos;

                isFacingRight = false;
            }
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            if (!isFacingRight)
            {
                Vector3 attackPos = attackCheck.localPosition;
                attackPos.x *= -1;
                attackCheck.localPosition = attackPos;

                isFacingRight = true;
            }
        }

    }

    private void Reset()
    {
        if (transform.position.y < -6)
        {
            
            

            ammunition = 1;
        }
    }
}
