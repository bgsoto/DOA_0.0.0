using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intel", menuName = "ScriptableObjects/New Intel", order = 2)]
public class Intel : ScriptableObject
{
    public string intelTitle;
    [TextArea(5, 20)]
    public string intelText;
    public string intelAuthor;
}
