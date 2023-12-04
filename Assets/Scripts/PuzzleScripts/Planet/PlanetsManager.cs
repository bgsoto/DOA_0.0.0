using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    private string currentInput;
    private int currentInputLength;
    [SerializeField]
    private string correctInput;
    [SerializeField]
    private int codeLength;
    public float[] codes;
    public string[] names;


    // public static Action planetInputNew; //action called when any planet is interacted with
    public static Action planetCorrect; //action called upon correct code input
    public static Action<string> planetWrong; //action called when wrong code input
    public static Action<string> birthdayCrew;
    private void OnEnable()
    {
        PlanetInteract.UpdateInput += InputHandler;
    }
    private void OnDisable()
    {
        PlanetInteract.UpdateInput -= InputHandler;
    }
    private void Start()
    {
        int code = UnityEngine.Random.Range(0, codes.Length);
        correctInput = codes[code].ToString();
        Debug.Log("correct planet code is " + codes[code].ToString());
        birthdayCrew?.Invoke(names[code]);
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
