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

    /* Subscription: DisplayManager. */
    public void PressKey() { OnKeyPressed?.Invoke(keyText); }
    public void PressEnter() { OnEnterPressed?.Invoke(); }
    public void PressReturn() { OnReturnPressed?.Invoke(); }
}
