using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Note", menuName = "ScriptableObjects/New Note", order = 1)]
public class Note : ScriptableObject
{
    public string noteTitle;
    public string noteText;
}
