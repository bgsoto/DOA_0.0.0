using System;
using TMPro;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public static Action<string> OnCodeEntered;

    private TMP_Text[] displayTextList;
    
    private int currentDisplay = 0;
    private bool displayIsFull = false;
    private bool displayIsEmpty = true;
    private string enteredCoordinates = "";

    private void Awake()
    {
        /*
        * Subscribes to the events in Key script.
        * Subscribes to OnWrongCoor event in ActivateRig script.
        * Invokes: AddDigit(), RemoveDigit(), EnterCode(), ResetDisplay()
        */
        KeyPress.OnKeyPressed += AddDigit;
        KeyPress.OnReturnPressed += RemoveDigit;
        KeyPress.OnEnterPressed += EnterCode;
        ActivateRig.OnWrongCoor += ResetDisplay;
    }

    private void OnDisable()
    {
        /* 
         * Unsubscribes from events in Key script. 
         * Unsubscribes from event in ActivateRig script.
         */
        KeyPress.OnKeyPressed -= AddDigit;
        KeyPress.OnReturnPressed -= RemoveDigit;
        KeyPress.OnEnterPressed -= EnterCode;
        ActivateRig.OnWrongCoor -= ResetDisplay;
    }

    private void Start()
    {
        displayTextList = GetComponentsInChildren<TMP_Text>();
    }

    /* 
     * Adds a digit to the current string (currentDisplay) if the list (displayTextList) is not full.
     * If true, sets the display string list as not empty.
     */
    public void AddDigit(string digit)
    {        
        if (!displayIsFull)
        {
            /* 
             * Checks if the current string is than less than three digits.
             * 
             * If true, adds digit to the current string.
             * 
             * If false, switches to the next string in the list. If the index value (currentDisplay) 
             * is more than what the display list has, sets the index value to the last position (string)
             * of the list and sets the list as full. 
             * 
             * Otherwise, adds digit to the next string in the list.
             */
            if (displayTextList[currentDisplay].text.Length < 3)
            {
                displayTextList[currentDisplay].text += digit;
            }
            else
            {
                currentDisplay++;

                if (currentDisplay >= displayTextList.Length)
                {
                    currentDisplay = displayTextList.Length - 1;
                    displayIsFull = true;
                }
                else
                {
                    displayTextList[currentDisplay].text += digit;    
                }
            }

            displayIsEmpty = false;
        }
    }

    /* 
     * Removes a digit from the current string (currentDisplay) if the current string's length is more than zero. 
     * If true, enables the list (displayTextList) as not full. 
     */
    public void RemoveDigit()
    {
        if (!displayIsEmpty)
        {
            /* Removes the last digit of the current sting. */
            displayTextList[currentDisplay].text = displayTextList[currentDisplay].text[..^1];

            /* 
             * If the current string's length is less than or equal to zero, sets the index value (currentDisplay)
             * to the previous string in the list.
             * 
             * If the index value is less than zero, sets the index value to 0 and sets the list as empty.
             */
            if (displayTextList[currentDisplay].text.Length <= 0)
            {
                currentDisplay--;

                if (currentDisplay < 0)
                {
                    currentDisplay = 0;
                    displayIsEmpty = true;
                }
            }

            displayIsFull = false;
        }  
    }

    public void EnterCode()
    {       
        foreach (TMP_Text text in displayTextList) { enteredCoordinates += text.text; }
        OnCodeEntered?.Invoke(enteredCoordinates);
    }

    private void ResetDisplay()
    {
        foreach (TMP_Text text in displayTextList) { text.text = ""; }
        currentDisplay = 0;
        displayIsFull = false;
        displayIsEmpty = true;
        enteredCoordinates = "";
        Debug.Log("INCORRECT CODE");
    }
}
