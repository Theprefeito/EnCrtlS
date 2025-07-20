using System;
using UnityEngine;






public enum STATE
{
    DISABLE,
    WAITING,
    TYPING,
}
public class DialoguesFunction : MonoBehaviour
{
    public GameObject dialogueBox;
    public DialoguesData dialoguesData;
   
    
    
    
    int currentText = 0;
    bool finished = false;

     AnimationText animationText;
    
    
    STATE state;

    private void Awake()
    {
        animationText = FindFirstObjectByType<AnimationText>();
        animationText.TypeFinished = OntypeFinisehd;
        
    }


    void Start()
    {
        state = STATE.DISABLE;
        dialogueBox.SetActive(false);
    }

   
    void Update()
    {
        if (state == STATE.DISABLE) return;
        switch (state)
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
            
            
            
            
            
        }
        
    }


   public void Next()
    {
        
        dialogueBox.SetActive(true);
        animationText.fullText = dialoguesData.dialogueScript[currentText++].dialogueText;

        if (currentText == dialoguesData.dialogueScript.Count) finished = true;
        {
            dialogueBox.SetActive(false);
        }

        animationText.StartTyping();
        state = STATE.TYPING;

    }


    public void OntypeFinisehd()
    {
        state = STATE.WAITING;
        dialogueBox.SetActive(true);
    }
   
   
   void Waiting()
    {
      
        dialogueBox.SetActive(true);
       
        if (Input.GetKeyDown(KeyCode.Return ))
        {
             if (!finished)
                    {
                        Next();
                    }
                    else
                    {
                      
                        state = STATE.DISABLE;
                        currentText = 0; 
                        finished = false;
                        dialogueBox.SetActive(false);
            }
        }
        
       
        
    }

    void Typing()
    {
      dialogueBox.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
            
        {
            animationText.Skip();
            state = STATE.WAITING;
        }
    }
}

