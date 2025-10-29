using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PanelVolume;
    [SerializeField] GameObject PanelRebinding;
    [SerializeField] GameObject PanelBase;
    
    public Transform pauseMenu;
    public TMP_Dropdown dropdown; //usar depois
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                /*
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
                */
            }
            else
            {
                {
                    pauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
                /*
                if (!audio.isPlaying)

                {
                    audio.Play();
                }
            */
                }
        }
    }

    
    public void Audio()
    {
        PanelVolume.gameObject.SetActive(true);
        PanelBase.gameObject.SetActive(false);
    }
    
    public void ReturnAudio()
    {
        PanelVolume.gameObject.SetActive(false);
        PanelBase.gameObject.SetActive(true);
    }

    public void Buttons()
    {
        PanelRebinding.gameObject.SetActive(true);
        PanelBase.gameObject.SetActive(false);
    }

    public void ReturnButtons()
    {
        PanelRebinding.gameObject.SetActive(false);
        PanelBase.gameObject.SetActive(true);
    }


}
