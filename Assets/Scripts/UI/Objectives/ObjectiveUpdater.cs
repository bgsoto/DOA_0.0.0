using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUpdater : MonoBehaviour
{
    [SerializeField] private int stageToSet; //stage of objective to set, see objective scriptable objects (increments by 10)

    public static Action<int> UpdateObjectives;
    public static Action<int> UpdateObjectives2;//for objective 2 if required.

    // updates objectives/clues based on quest stage. call these on objectives being completed.

    public void Update1() //call this when an objective is complete or clue is found through unity events, otherwise use the static action
    {
        UpdateObjectives?.Invoke(stageToSet);
    }
    public void Update2()
    {
        UpdateObjectives2?.Invoke(stageToSet);
    }
}
