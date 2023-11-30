using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using StarterAssets;
using System;

public class IntelMonitorManger : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionText = "View Intel";
    private bool inMenu = false;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private Collider coll;
    private StarterAssetsInputs _input;

    public static Action<bool> inIntelMenu;
    /* Not used */
    private ItemData itemData;
    private bool pickable;
    private void Start()
    {
        _input = FindObjectOfType<StarterAssetsInputs>();
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        if (!inMenu) { return; }
        if(_input.move.magnitude > 0)
        {
            inMenu = false;
            GetComponentInChildren<CanvasGroup>().interactable = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            coll.enabled = true;
            vcam.m_Priority = -10;
            inIntelMenu?.Invoke(false);
        }
    }
    public void Interact()
    {
        inMenu = true;
        GetComponentInChildren<CanvasGroup>().interactable = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        coll.enabled = false;
        vcam.m_Priority = 100;
        inIntelMenu?.Invoke(true);
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}
