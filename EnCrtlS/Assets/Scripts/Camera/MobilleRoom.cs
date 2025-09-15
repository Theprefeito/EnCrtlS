using UnityEngine;
using Unity.VisualScripting;

public class MobilleRoom : MonoBehaviour
{
    [SerializeField] Transform player; 
    [SerializeField] float minX, maxX; 
    [SerializeField] float minY, maxY; 
    public Vector2 startPosition;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position; //Salvando a poisção inicial
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDead();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            float wayX = Mathf.Clamp(player.position.x, minX, maxX); //a variavel wayX vai possuir qual quer valor com base na posição do player entre minX e maxX
            float wayY = Mathf.Clamp(player.position.y, minY, maxY); // a mesma coisa do de cima só que para o Y

            transform.position = new Vector3(wayX, wayY, transform.position.z); //faz com que a Sala siga o player com base no "caminho"
        }

        

    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = startPosition; //Caso o player saia da sala a posição da sala é resetada
        }
    }
    */
    void PlayerDead()
    {
        if (player.position.y < -6f)
        {
            transform.position = startPosition; //Caso o player morra a posição da sala é resetada
        }
    }
    
}
