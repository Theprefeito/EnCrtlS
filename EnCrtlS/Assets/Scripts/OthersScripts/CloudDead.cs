using UnityEngine;

public class CloudDead : MonoBehaviour
{
    [SerializeField] float timeToDie;
    [SerializeField] Transform player;
    private float startTimetodie;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTimetodie = timeToDie;
        GameObject objetoComTag = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeToDie -= Time.deltaTime;
        
        Dead();
    }

    private void Dead()
    {
        GameObject objetoComTag = GameObject.FindGameObjectWithTag("Player");

        if (timeToDie < 0 || objetoComTag.transform.position.y < -6f)
        {
            gameObject.SetActive(false);
            timeToDie = 6;
        }

        else if(objetoComTag.transform.position.y > -6f)
        {
            gameObject.SetActive(true);
        }
       
    }

   

}
