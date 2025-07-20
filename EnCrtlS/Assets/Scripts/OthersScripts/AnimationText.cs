using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnimationText : MonoBehaviour
{

    public Action TypeFinished;
    
    public float delay;
    public TextMeshProUGUI textDialogo;
   
    public string fullText;
    
    Coroutine coroutine;
    void Start()
    {
       
    }

    public void StartTyping()
    {
        coroutine = StartCoroutine(TextAnimation());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TextAnimation()
    {
        textDialogo.text = fullText;
        textDialogo.maxVisibleCharacters = 0;
        for (int i = 0; i <= textDialogo.text.Length; i++)
        {
            textDialogo.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
        }
            
        TypeFinished?.Invoke();
    }

    public void Skip()
    { 
        StopCoroutine(coroutine);
     textDialogo.maxVisibleCharacters = textDialogo.text.Length;
    }
    
    
}
