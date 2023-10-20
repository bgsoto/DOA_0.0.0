using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUpdater : MonoBehaviour
{
    [SerializeField] private int objectiveIndex; //index of objective to set, see objective scriptable object
    [SerializeField] private int clueIndex; //index of clue to set, see objective scriptable object
    [SerializeField] private bool updateObj;
    [SerializeField] private bool updateClue;

    public static Action<int> UpdateObjectives;
    public static Action<int> UpdateClues;

    // updates objectives/clues based on index. call these on objectives being completed.

    public void UpdateAll() //call this when an objective is complete or clue is found
    {
        if(updateObj) { UpdateObjectives.Invoke(objectiveIndex); }
        if(updateClue) { UpdateClues.Invoke(clueIndex); }
    }
}
