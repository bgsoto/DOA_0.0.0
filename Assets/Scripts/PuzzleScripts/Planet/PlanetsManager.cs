using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    private List<int> currentInputs = new List<int>();
    [SerializeField]
    private List<int> correctInputs = new List<int>();
    [SerializeField]
    private int codeLength;
   // public static Action planetInputNew; //action called when any planet is interacted with
    public static Action planetCorrect; //action called upon correct code input
    public static Action planetWrong; //action called when wrong code input
    private void OnEnable()
    {
        PlanetInteract.UpdateInput += InputHandler;
    }
    private void OnDisable()
    {
        PlanetInteract.UpdateInput -= InputHandler;
    }
    void InputHandler(int input)
    {
        currentInputs.Add(input);
        if (currentInputs.Count >= codeLength) {
        for (int i = 0; i < currentInputs.Count; i++)
            {
                if (currentInputs[i] != correctInputs[i]) {
                    planetWrong?.Invoke();
                    currentInputs.Clear();
                    Debug.Log("wrong code.");
                    return;
                    //resets code and stops operation if any input is wrong
                }
            }
            planetCorrect?.Invoke();
            Debug.Log("correct code.");
        }//invokes correct code action if no inputs are wrong
    }
}
