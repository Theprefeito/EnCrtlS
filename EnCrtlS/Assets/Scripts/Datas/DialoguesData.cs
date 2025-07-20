using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct Dialogue
{
    public string personName;
    [TextArea(3, 10)]
    public string dialogueText;
}

[CreateAssetMenu(fileName = "DialoguesData", menuName = "ScriptableObjects/dialogueScript", order = 1)]
public class DialoguesData : ScriptableObject
{
   public List<Dialogue> dialogueScript;
   
}
