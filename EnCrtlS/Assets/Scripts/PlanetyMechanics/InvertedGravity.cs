using UnityEngine;

public class InvertedGravity : MonoBehaviour
{
    private Rigidbody2D rigPlayer;
    private SpriteRenderer srPlayer;
    private bool invertedGravity = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            invertedGravity = !invertedGravity;

            rigPlayer.gravityScale *= -1;

            if (invertedGravity)
            {
                srPlayer.flipY = true;
            }

            else
            {
                srPlayer.flipY = false;
            }

        }
    }

}