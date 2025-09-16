using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAbyss : MonoBehaviour
{
    public float limitsX;
    public float limitsY;
    public bool playerIsDead = false;
    [SerializeField] AudioClip soundDeath;
    [SerializeField] Animator transitionAnim;
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
            StartCoroutine(PlayerDie());
        }
    }

    public IEnumerator PlayerDie()
    {
        SoundsScript.instance.SoundExecuter(soundDeath);
        transform.position = respawnPoint.transform.position;
        playerIsDead = true;
        yield return new WaitForSeconds(0.5f);
        playerIsDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            
            //GetComponent<Animator>().SetTrigger("Dead");
            //GetComponent<PlayerMovement>().enabled = false;


            StartCoroutine(PlayerDie());
        }

    }

}
