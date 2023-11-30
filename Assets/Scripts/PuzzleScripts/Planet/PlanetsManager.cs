using System;
using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    private string currentInput;
    private int currentInputLength;
    [SerializeField]
    private string correctInput;
    [SerializeField]
    private int codeLength;

    // public static Action planetInputNew; //action called when any planet is interacted with
    public static Action planetCorrect; //action called upon correct code input
    public static Action<string> planetWrong; //action called when wrong code input
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
        currentInput = currentInput + input;
        currentInputLength++;
        if (currentInputLength >= codeLength)
        {
            if (currentInput != correctInput)
            {
                planetWrong?.Invoke(currentInput);
                Debug.Log("wrong code." + currentInput);
                currentInput = "";
                currentInputLength = 0;
                return;
                //resets code and stops operation if any input is wrong
            }
            else
            {
                planetCorrect?.Invoke();
                Debug.Log("correct code.");
            }
        }
    }//invokes correct code action if no inputs are wrong
}
