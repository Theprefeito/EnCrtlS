using UnityEngine;

public class SoundsScript : MonoBehaviour
{
    public static SoundsScript instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundExecuter(AudioClip sons)
    {
        audioSource.PlayOneShot(sons);
    }

}
