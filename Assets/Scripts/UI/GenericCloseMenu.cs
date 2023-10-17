using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GenericCloseMenu : MonoBehaviour
{
    public static Action CloseCurrentMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }
    public void CloseMenu()
    {
        /* Subscription: UIManager script. */
        CloseCurrentMenu?.Invoke();
    }
}
