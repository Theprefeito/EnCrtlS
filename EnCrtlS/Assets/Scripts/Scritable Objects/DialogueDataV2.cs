using UnityEngine;



[System.Serializable]
public class MultipleDialogue
{
    [TextArea(3, 10)]
    public string textDialogue;

    public string npcName;
    public Sprite npcIcon;
}







[CreateAssetMenu(fileName = "DialoguesDataV2" , menuName = "ScriptableObjects/NPCdialogue", order = 1)]
public class DialogueDataV2 : ScriptableObject
{

  public MultipleDialogue[] lines;

}
