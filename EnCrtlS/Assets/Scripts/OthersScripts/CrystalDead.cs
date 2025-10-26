using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CrystalDead : MonoBehaviour
{
    [SerializeField] string tagPlayer;
    [SerializeField] float respawnTime;
    [SerializeField] Transform player;
    private bool onDead = false;
    private SpriteRenderer srCrystal;
    JupiterCloud jupiter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jupiter = GetComponent<JupiterCloud>();
        srCrystal = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {       
        EnableSrCrystal();
    }

    private void EnableSrCrystal()
    {

        if (onDead)
        {
            srCrystal.enabled = false;
        }

        else
        {
            srCrystal.enabled = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagPlayer) && !onDead)
        {           
            collision.GetComponent<JupiterCloud>().ammunition = 1; //Seta a munição para 1

            
            StartCoroutine(crystalMissing()); // Destroi o power-up
        }
    }

    private IEnumerator crystalMissing()
    {        
        onDead = true;
        yield return new WaitForSeconds(respawnTime);
        onDead = false;        
    }

}
