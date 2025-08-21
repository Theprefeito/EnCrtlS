using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
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



    private Coroutine typingCoroutine;
    bool isTyping;

    void Start()
    {
        panel.gameObject.SetActive(false);
        typingCoroutine = null;
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
                FindAnyObjectByType<PlayerMovement>().speedPlayer = 0;
                StartDialogue();
            }
            else
            {
                if(!isTyping)
                {
                    Next();
                }

                
            }
        }
    }

    private void Next()
    {
        dialogueIndex++;


        if(dialogueIndex < dialogueData.lines.Length)
        {
            StartCoroutine(DialogueShow());
        }
        else
        {
            panel.gameObject.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            FindAnyObjectByType<PlayerMovement>().speedPlayer = 5f;
        }

    }




    private IEnumerator DialogueShow()
    {


        isTyping = true;

        MultipleDialogue line = dialogueData.lines[dialogueIndex];

        dialogueText.text = "";
        nameInBox.text = line.npcName;
        icon.sprite = line.npcIcon;


        foreach (char letter in line.textDialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
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
