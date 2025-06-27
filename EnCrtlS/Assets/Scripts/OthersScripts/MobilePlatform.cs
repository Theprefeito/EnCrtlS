using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    [SerializeField] float platformSpeed;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform player;

    public Vector3 startPosition;
    public bool isVertical; //deve ser definido no inspector
    public bool moveDown;
    public bool moveRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //caso queira a plataforma andando no eixo Y
        if (isVertical)
        {
            Vertical();
        }

        //caso queira a plataforma andando no eixo X
        else
        {
            Horizontal();
        }
     
        if(player.transform.position.y < -7)
        {
            Reset();
        }
    }


    void Vertical()
    {
        if(transform.position.y > pointA.position.y)
        {
            moveDown = true;
        }
    
        if(transform.position.y < pointB.position.y)
        {
            moveDown = false;
        }

        if (moveDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - platformSpeed * Time.deltaTime);
        }

        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + platformSpeed * Time.deltaTime);
        }
    }

    void Horizontal()
    {
        if(transform.position.x < pointA.position.x)
        {
            moveRight = true;
        }
    
        if(transform.position.x > pointB.position.x)
        {
            moveRight = false;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + platformSpeed * Time.deltaTime, transform.position.y);
        }

        else
        {
            transform.position = new Vector2(transform.position.x - platformSpeed * Time.deltaTime, transform.position.y);
        }

    }

    private void Reset()
    {
        transform.position = startPosition;
    }
}
