using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private ItemData trackerData;
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    public void Interact() { Destroy(gameObject); }

    public void Use() { return; }
    public ItemData ItemData { get { return trackerData; } set { trackerData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
