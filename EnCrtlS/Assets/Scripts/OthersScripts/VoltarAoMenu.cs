using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarAoMenu : MonoBehaviour
{
    [SerializeField] string menuSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(voltarMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator voltarMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(menuSceneName);
    }
}
