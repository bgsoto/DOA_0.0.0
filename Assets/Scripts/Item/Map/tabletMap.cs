using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tabletMap : MonoBehaviour, IInteractable
{


    [Header("Relationships")]
    private AudioSource source;

    [Header("Settings")]
    [SerializeField] private ItemData mapData;
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Interact(){Destroy(gameObject);}
    public void Use()
    {

    }

    public ItemData ItemData { get { return mapData; } set { mapData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
    public bool itemOutline { get { return itemOutline; } set { itemOutline = value; } }
}

