using Unity.VisualScripting;
using UnityEngine;

public class CrystalDead : MonoBehaviour
{
    [SerializeField] string tagPlayer;
    [SerializeField] Transform player;
    [SerializeField] Vector3 startPosition;
    JupiterCloud jupiter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        jupiter = GetComponent<JupiterCloud>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -6)
        {
            transform.position = startPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagPlayer))
        {
            // Aqui você pode aplicar o efeito do power-up:
            // exemplo: dar vida, velocidade, pulo duplo etc.
            //Debug.Log("Power-up coletado!");
            collision.GetComponent<JupiterCloud>().ammunition = 1;

            // Destroi o power-up
            transform.position = new Vector3(-800, -800, 0);
        }
    }

}
