using System;
using UnityEngine;

public class MenuDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private Menu menuToDisplay;

    /* Not used */
    private ItemData itemData;
    private bool pickable;
    private string actionText;

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

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
