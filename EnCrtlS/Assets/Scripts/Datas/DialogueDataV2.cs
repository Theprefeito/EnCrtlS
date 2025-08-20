using UnityEngine;


[CreateAssetMenu(fileName = "DialoguesDataV2" , menuName = "ScriptableObjects/NPCdialogue", order = 1)]
public class DialogueDataV2 : ScriptableObject
{

    [TextArea(3, 10)]
    public string[] dialogueText;

    public string npcName;
    public Sprite npcIcon;

}
