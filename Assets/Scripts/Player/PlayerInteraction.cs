using DG.Tweening;
using EPOOutline;
using System;
using TMPro;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private ItemHolder itemHolder;
    [SerializeField] private Image reticle;
    [SerializeField] private Outlinable itemOutline;

    [Header("Settings")]
    [SerializeField] private float rayLength;
    [SerializeField] private GameObject interactText;

    public static Action<bool, int> onAllKeysCollected;
    public static Action<bool, int> onArtifactCollected;
   
    private Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    private IInteractable interactableObject;
    private bool inMenu = false;
    private bool canInteract = false;
    public int keysCollected = 0;
    
    private void OnEnable()
    {
        ShowKeypad.DisableControls += DisableRayCast;
        Key.onKeyCollected += AddKey;
        Artifact.onArtifactCollected += AddArtifact;
        SettingsOpener.PausedGame += DisableRayCast;
    }

    private void OnDisable()
    {
        ShowKeypad.DisableControls -= DisableRayCast;
        Key.onKeyCollected -= AddKey;
        Artifact.onArtifactCollected -= AddArtifact;
        SettingsOpener.PausedGame -= DisableRayCast;
    }

    private void Update()
    {
        if (!inMenu)
        {
            ObjectDetector();

            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
    }

    private void DisableRayCast(bool value) { inMenu = value; }

    private void ObjectDetector()
    {
        /* Create a ray from center of the screen. Prevents cursor from unlocking. */
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        /* Determines the color of the reticle if facing hovering over an object that is interactable. */
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {

          /*  if (hit.collider.gameObject.CompareTag("Item"))
            {
                itemOutline = hit.collider.gameObject.GetComponent<Outlinable>();
                itemOutline.enabled = true;
                Debug.Log("item detected");
            }
            else
            {
                if (itemOutline != null)
                {
                    itemOutline.enabled = false;
                    itemOutline = null;
                }              
            }
          */

            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                IInteractable hitInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
                
                interactableObject = hitInteractable;
                reticle.color = Color.green;
                interactText.GetComponent<TMP_Text>().text = $"{interactableObject.ActionText} (F)";
                interactText.SetActive(true);
                canInteract = true;
             
            }
            else
            {
                reticle.color = Color.white;
                interactText.SetActive(false);
                canInteract = false;
             
            }
        }
        else
        {
            reticle.color = Color.white;
            interactText.SetActive(false);
            canInteract = false;

            if (itemOutline != null)
            {
                itemOutline.enabled = false;
                itemOutline = null;  // Reset the variable
            }

        }

    
    }

    
private void Interact()
    {
        if (canInteract)
        {
            if (interactableObject.Pickable)
            {
                itemHolder.AddToInventory(interactableObject.ItemData);
                interactableObject.Interact();              
            }
            else
            {
                interactableObject.Interact();
            }
        }
    }

    private void AddKey(int value)
    {
        keysCollected++;
        
        if (keysCollected == 2) 
        { 
            /* Subscription: ObjectiveManager */
            onAllKeysCollected?.Invoke(false, 30);
        }
    }

    public void AddArtifact() 
    {
        /* Subscription: ObjectiveManager */
        onArtifactCollected?.Invoke(false, 40); 
    }
}
