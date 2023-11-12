using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject keypadUI;

    public void ShowKeypad(bool value)
    {
        keypadUI.SetActive(value);
    }

    public void HideKeypad()
    {
        keypadUI?.SetActive(false);
    }
}
