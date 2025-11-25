using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinalFase : MonoBehaviour
{
    public string proxFase;
    public Animator transitionAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transitionAnim.SetTrigger("isClose");
            StartCoroutine(endTransition());
        }
    }

    private IEnumerator endTransition()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(proxFase);
    }
}
