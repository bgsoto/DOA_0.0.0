using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Safe : MonoBehaviour, IInteractable
{
    [Header("Relationships")]
    [SerializeField] PlayerInteraction playerinteractionScript;

    [Header("Settings")]
    [SerializeField] bool allkeysFound;
    [SerializeField] private string actionText;
    
    /* Not used */
    private ItemData safe;
    private bool pickable = false;


    public void Interact()
    {
        int keysCollected = playerinteractionScript.keysCollected;
            if (keysCollected == 2)
            {
                allkeysFound = true;
                if (allkeysFound) {Destroy(gameObject);}
            }
    }


    public void Use() { return; }
    public ItemData ItemData { get { return safe; } set { safe = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }



}
