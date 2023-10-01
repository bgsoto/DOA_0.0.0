using System;
using TMPro;
using UnityEngine;

public class KeyPress : MonoBehaviour
{
    private string keyText;

    public static Action<string> OnKeyPressed;
    public static Action OnReturnPressed;
    public static Action OnEnterPressed;

    private void Awake()
    {
        /* Gets all TMP_Text objects from this object's childern. */
        keyText = GetComponentInChildren<TMP_Text>().text;
    }

    public void PressKey()
    {
        /*
        * Calls all functions subscribed to this event.
        * Subscription: DisplayManager.
        */
        OnKeyPressed?.Invoke(keyText);
    }

    public void PressEnter()
    {
        /*
        * Calls all functions subscribed to this event.
        * Subscription: DisplayManager.
        */
        OnEnterPressed?.Invoke();
    }

    public void PressReturn()
    {
        /*
        * Calls all functions subscribed to this event.
        * Subscription: DisplayManager.
        */
        OnReturnPressed?.Invoke();
    }
}
