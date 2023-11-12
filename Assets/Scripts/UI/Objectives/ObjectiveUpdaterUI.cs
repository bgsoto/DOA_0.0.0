using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUpdaterUI : MonoBehaviour
{
    //use this script for UI elements like buttons or inputs that update the objective.
    [SerializeField] private int stageToSet; //stage of objective to set, see objective scriptable objects (increments by 10)
    [SerializeField] private bool isObjective2;

    public static Action<bool, int> UpdateObjectivesUI;

    // updates objectives/clues based on quest stage. call these on objectives being completed.

    public void UpdateObjectiveUI() //call this when an objective is complete or clue is found through unity events/UI
    {
        UpdateObjectivesUI?.Invoke(isObjective2, stageToSet);
    }
}
