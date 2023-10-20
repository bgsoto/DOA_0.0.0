using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objectives", menuName = "ScriptableObjects/New Objectives", order = 2)]
public class ObjectiveInfo : ScriptableObject
{
    [TextArea(5, 20)]
    public List<string> objectiveDesc;
    [TextArea(5, 20)]
    public List<string> clueDesc;
}
