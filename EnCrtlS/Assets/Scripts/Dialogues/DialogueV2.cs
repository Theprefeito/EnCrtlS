using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;
public class DialogueV2 : MonoBehaviour
{
    public DialogueDataV2 dialogueData;

    public int dialogueIndex;

    public GameObject panel;
    public TMP_Text dialogueText;

    public TMP_Text nameInBox;
    public Image icon;
   


    public bool startDialogue;
    public bool speakStart;

    public AudioSource voice;

    bool isTyping;

   
    void Start()
    {
        panel.gameObject.SetActive(false);
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            speakStart = true;
          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speakStart = false;
    }
    // Update is called once per frame
  
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && speakStart)
        {
            if (!startDialogue)
            {
               
                FindAnyObjectByType<PlayerMovement>().canMove = false;
              
                   StartDialogue();
            }
            else if (!isTyping)
            {
               Next();
            }
        }
    }

    private void Next()
    {
        dialogueIndex++;


        if(dialogueIndex < dialogueData.lists.Length)
        {
            StartCoroutine(DialogueShow());
        }
        else
        {
            panel.gameObject.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            FindAnyObjectByType<PlayerMovement>().canMove = true ;
        }

    }




    private IEnumerator DialogueShow()
    {


        isTyping = true;

        MultipleDialogue lists = dialogueData.lists[dialogueIndex];

        dialogueText.text = "";
        nameInBox.text = lists.npcName;
        icon.sprite = lists.npcIcon;                            //Basicamente o conjunto de variaveis deste script que retornam algo no Scriptable Object
        voice.clip = lists.voiceSound;
           
        
        
        int tocarPorPartes = 0;
       
        foreach (char letter in lists.textDialogue)
        {

             dialogueText.text += letter;
         
            if(lists.voiceSound != null && tocarPorPartes % 2 == 0) //Faz com que a cada 2 letras toque o audio
            {
                voice.PlayOneShot(lists.voiceSound);
            }
           
            tocarPorPartes++;
           
            yield return new WaitForSeconds(0.05f); //Tempo que a letra aparece 
        }
      
        isTyping = false;
    }



    private void StartDialogue()
    {
       
        startDialogue = true;
        dialogueIndex = 0;
        panel.gameObject.SetActive(true);
        StartCoroutine(DialogueShow());
    }


    
}
