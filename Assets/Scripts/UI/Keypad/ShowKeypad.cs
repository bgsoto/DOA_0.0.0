using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShowKeypad : MonoBehaviour, IInteractable
{
    [SerializeField] private UIManager UIManager;

    public static Action<bool> DisableControls;

    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    private bool canInteract = false;
    
    /* Not used */
    private ItemData itemData;

    private void OnEnable()
    {
        KeypadDisplayManager.OnCorrectCoor += HideCanvas;
    }

    private void OnDisable()
    {
        KeypadDisplayManager.OnCorrectCoor -= HideCanvas;
    }

    private void Awake()
    {
        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void HideCanvas()
    {
        canInteract = false;
        UIManager.ShowKeypad(canInteract);

        /* Subscription: PlayerInteraction, FirstPersonController, StartAssestsInput */
        DisableControls?.Invoke(canInteract);
    }

    public void Interact()
    {
        if (canInteract)
        {
            UIManager.ShowKeypad(canInteract);

            /* Subscription: PlayerInteraction, FirstPersonController, StartAssestsInput */
            DisableControls?.Invoke(canInteract);
        }
    }

    public void Use() { return; }

    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
}
