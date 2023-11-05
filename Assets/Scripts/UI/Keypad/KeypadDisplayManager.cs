using System;
using TMPro;
using UnityEngine;

public class KeypadDisplayManager : MonoBehaviour
{
    [SerializeField] private string dimensionalCoordinates;

    public static Action OnCorrectCoor;

    private TMP_Text displayText;
    private bool displayIsFull = false;
    private bool displayIsEmpty = true;

    private void OnEnable()
    {
        /* Subscribes to event(s). */
        KeyPress.OnKeyPressed += AddDigit;
        KeyPress.OnReturnPressed += RemoveDigit;
        KeyPress.OnEnterPressed += EnterCode;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        KeyPress.OnKeyPressed -= AddDigit;
        KeyPress.OnReturnPressed -= RemoveDigit;
        KeyPress.OnEnterPressed -= EnterCode;
    }

    private void Start()
    {
        displayText = GetComponentInChildren<TMP_Text>();
    }

    /* 
     * Adds a digit to the string (displayText.text) if the string is not full.
     * If true, sets the display string list as not empty.
     */
    public void AddDigit(string digit)
    {        
        if (!displayIsFull)
        {
            /* 
             * Checks if the string is than less than five digits.
             * If true, adds digit to the current string.
             * If false, does not add a digit and sets the list as full. 
             */
            if (displayText.text.Length < 5)
            {
                displayText.text += digit;
                displayIsEmpty = false;
            }
            else
            {
                displayIsFull = true;
            }
        }
    }

    /* 
     * Removes a digit from the string (displayText.text) if the string's length is more than zero. 
     * If true, sets the string as not full. 
     */
    public void RemoveDigit()
    {
        if (!displayIsEmpty)
        {
            /* 
             * Checks if the string's length is than less zero.
             * If true, removes digit from the string.
             * If false, does not remove a digit and sets the list as empty. 
             */
            if (displayText.text.Length > 0)
            {
                /* Shorthand: Removes the last digit of the current sting. */
                displayText.text = displayText.text[..^1];
                displayIsFull = false;
            }
            else
            {
                displayIsEmpty = true;
            }
        }  
    }

    public void EnterCode()
    {
        if (displayText.text == dimensionalCoordinates)
        {
            /* Subscription: ShowKeypad script. */
            OnCorrectCoor?.Invoke();
            Debug.Log("CORRECT CODE");
        }
        else
        {
            ResetDisplay();
        }
    }

    /* Reset values. */
    private void ResetDisplay()
    {
        displayText.text = "";
        displayIsFull = false;
        displayIsEmpty = true;
        Debug.Log("INCORRECT CODE");
    }
}
