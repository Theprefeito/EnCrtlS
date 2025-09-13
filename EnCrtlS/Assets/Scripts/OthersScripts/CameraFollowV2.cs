using UnityEngine;

public class CameraFollowV2 : MonoBehaviour
{
    [SerializeField] Transform player;
    public float transtionTime = 0.3f;
    private Vector3 transtionSpeed = Vector3.zero;
    private Transform currentRoom;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if(currentRoom != null) // se a sala atual existe ele vai executar o codigo abaixo
        {
            Vector3 center = new Vector3(currentRoom.position.x, currentRoom.position.y, transform.position.z); // serve para dizer onde vai ser o centro da camera
            transform.position = Vector3.SmoothDamp(transform.position, center, ref transtionSpeed, transtionTime); // serve para mover a camera quando troca de sala
        }
    }

    public void SetCurrentRoom(Transform room)
    {
        currentRoom = room; // quando o player colide com uma nova sala essa sala é setada para a sala atual
    }

}
