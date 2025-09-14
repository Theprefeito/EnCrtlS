using System;
using UnityEngine;



[Serializable]
public class MultipleDialogue
{
    [TextArea(3, 10)]
    public string textDialogue;

    public string npcName;
    public Sprite npcIcon;
    public AudioClip voiceSound;
}







[CreateAssetMenu(fileName = "DialoguesDataV2" , menuName = "ScriptableObjects/NPCdialogue", order = 1)]
public class DialogueDataV2 : ScriptableObject
{

  public MultipleDialogue[] lists;

}
