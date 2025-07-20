using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    public Transform pauseMenu;
    public GameObject Optionsmenu;
    public new AudioSource audio;
    public TMP_Dropdown dropdown; //usar depois
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Escape))
        {
                
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;

                if (audio.isPlaying)
                {
                    audio.Stop();
                }
            }
            else
            {
                {
                    pauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }

                if (!audio.isPlaying)
                
                {
                    audio.Play();
                }
            }
           
        }
        
        
    }

    
    
    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        audio.Stop();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        Optionsmenu.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        
    }
    
    
    public void ExitOptions()
    {
       Optionsmenu.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }
    
    
    
    
}
