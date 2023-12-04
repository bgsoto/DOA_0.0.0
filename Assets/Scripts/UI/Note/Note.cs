using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Note", menuName = "ScriptableObjects/New Note", order = 1)]
public class Note : ScriptableObject
{
    public string noteTitle;
    public bool isObjective2;
    public bool isVariable;
    [TextArea(5, 20)]
    public string noteText;
    [TextArea(5, 20)]
    public string noteText2;
    public int objectiveStage = 0;
}