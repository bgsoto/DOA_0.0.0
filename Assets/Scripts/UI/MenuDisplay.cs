using System;
using UnityEngine;

public class MenuDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private Menu menuToDisplay;

    /* The "list" of the different UI menus. */ 
    private enum Menu
    {
        Menu0 = 0, 
        Menu1 = 1
    }
    
    public static Action<int> OnMenuEnter;

    public void Interact()
    {
        int menuIndex = (int)menuToDisplay;

        /* Subscription: UIManager */
        OnMenuEnter?.Invoke(menuIndex);
    }
}
