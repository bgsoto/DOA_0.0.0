using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIList;

    public static Action<bool> DisablePlayerControls;

    private Stack<int> UIStack = new Stack<int>();

    private void OnEnable()
    {
        /* Subscribes to event(s). */
        MenuDisplay.OnMenuEnter += DisplayMenu;
        KeypadDisplayManager.OnCorrectCoor += HideMenu;
        GenericCloseMenu.CloseCurrentMenu += HideMenu;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        MenuDisplay.OnMenuEnter -= DisplayMenu;
        KeypadDisplayManager.OnCorrectCoor -= HideMenu;
        GenericCloseMenu.CloseCurrentMenu -= HideMenu;
    }

    private void Start()
    {
        /* Locks cursor in the middle of the screen and hides the cursor. */
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /* 
     * Disables player controls when a menu is active. 
     * Uses a Stack data structure to manage the number of active menus.
     */
    private void DisplayMenu(int menuIndex)
    {
        UIStack.Push(menuIndex);
        UIList[menuIndex].SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        /* Subscription: PlayerCamera, PlayerMovement. */
        DisablePlayerControls?.Invoke(true);
    }

    /* Enables player controls if there are no active menus. */
    private void HideMenu()
    {
        UIList[UIStack.Pop()].SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (UIStack.Count <= 0)
        {
            /* Subscription: PlayerCamera, PlayerMovement. */
            DisablePlayerControls?.Invoke(false);
        }
    }
}
