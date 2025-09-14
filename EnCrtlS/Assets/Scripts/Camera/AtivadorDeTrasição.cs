using UnityEngine;

public class AtivadorDeTrasição : MonoBehaviour
{
    public GameObject transitionScene;

    private void Awake()
    {
        transitionScene.SetActive(true);
    }
}
