using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUpdater : MonoBehaviour
{
    [SerializeField] private int stageToSet; //stage of objective to set, see objective scriptable objects (increments by 10)

    public static Action<int> UpdateObjectives;

    // updates objectives/clues based on quest stage. call these on objectives being completed.

    public void UpdateAll() //call this when an objective is complete or clue is found through unity events, otherwise use the static action
    {
        UpdateObjectives?.Invoke(stageToSet);
    }
}
