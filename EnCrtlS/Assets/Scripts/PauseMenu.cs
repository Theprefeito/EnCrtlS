using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    
    public Transform pauseMenu;
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
    
    
    
}
