using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour, IInteractable
{
    [Header("Relationships")]
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject spot;

    [Header("Settings")]
    [SerializeField] private ItemData flashlightData;
    [SerializeField] private Material lens;
    [SerializeField] private bool isOn = false;
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    public void Interact() { Destroy(gameObject); }

    public void Use()
    {
        isOn = !isOn;

        if (isOn)
        {
            point.SetActive(true);
            spot.SetActive(true);
            lens.EnableKeyword("_EMISSION");
        }
        else
        {
            point.SetActive(false);
            spot.SetActive(false);
            lens.DisableKeyword("_EMISSION");
        }
    }

    public ItemData ItemData { get { return flashlightData; } set { flashlightData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
