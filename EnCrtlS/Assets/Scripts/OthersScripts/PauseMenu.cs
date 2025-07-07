using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public Transform pauseMenu;
    public GameObject Optionsmenu;
    
    
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
            }
            else
            {
                {
                    pauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
            }
            
        }
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
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
