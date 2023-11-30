using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveGame : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionText = "Save Game";
    /* Not used */
    private ItemData itemData;
    private bool pickable;
    public void Interact()
    {
        DataPersistenceManager.Instance.SaveGame();
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
