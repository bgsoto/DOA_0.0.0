using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Safe : MonoBehaviour, IInteractable
{
    [Header("Relationships")]
    [SerializeField] PlayerInteraction playerinteractionScript;
    [SerializeField] ObjectiveManager objectiveManager;

    [Header("Settings")]
    [SerializeField] bool allkeysFound;
    [SerializeField] private string actionText;
    
    /* Not used */
    private ItemData safe;
    private bool pickable = false;

    private void Start()
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }
    public void Interact()
    {
        int keysCollected = playerinteractionScript.keysCollected;
            if (keysCollected == 2)
            {
                allkeysFound = true;
                if (allkeysFound) {
                objectiveManager.UpdateObjective(false, 40);
                Destroy(gameObject);}
            }
    }


    public void Use() { return; }
    public ItemData ItemData { get { return safe; } set { safe = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }



}
