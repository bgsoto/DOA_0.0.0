using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private ItemData tabletData;
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    public void Interact() { Destroy(gameObject); }

    public void Use() { return; }
    public ItemData ItemData { get { return tabletData; } set { tabletData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}

   

