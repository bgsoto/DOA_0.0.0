using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntelInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionText = "";
    public Intel intel;

    /* Not used */
    private ItemData itemData;
    private bool pickable;

    private void Start()
    {
        if (IntelCollectionManager.collectedIntel.Contains(intel))
        {
            Debug.Log(intel.name + " already gathered. Destroying.");
            Destroy(this);
        }
        else
        {
            actionText = "Record Datastream\n" + intel.intelTitle;
        }
    }
    public void Interact()
    {
        IntelCollectionManager.collectedIntel.Add(intel);
        Destroy(this);
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
