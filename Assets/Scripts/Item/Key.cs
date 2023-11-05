using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    public static Action<int> onKeyCollected;

    /* Not used */
    private ItemData itemData;

    public void Interact()
    {
        /* Subscription: PlayerInteraction */
        onKeyCollected?.Invoke(1);
        Destroy(gameObject);
    }

    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
