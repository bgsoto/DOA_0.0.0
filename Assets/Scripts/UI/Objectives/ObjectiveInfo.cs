using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objectives", menuName = "ScriptableObjects/New Objectives", order = 2)]
public class ObjectiveInfo : ScriptableObject
{
    public string objectiveName;
    [TextArea(5, 20)]
    public string objectiveDescription;
    public int objectiveStage;
    [TextArea(5, 20)]
    public string clueDescription;
}
