using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMenager : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] string loadSceneName;
    [SerializeField] string mainMenuScene;
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] GameObject panelOptions;
    [SerializeField] GameObject gameName;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadSceneName);
    }

    public void ExitLoadGame()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Options()
    {
        panelMainMenu.SetActive(false);
        gameName.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void ClosedOptions()
    {
        panelMainMenu.SetActive(true );
        gameName.SetActive(true);
        panelOptions.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
