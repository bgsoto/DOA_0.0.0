using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class Rig : MonoBehaviour, IInteractable
{
    [Header("Relationships")]
    [SerializeField] private Transform rigParentTransform;

    [Header("Interface Settings")]
    [SerializeField] private ItemData rigData;
    [SerializeField] private bool pickable;

    [Header("Max Placement Distance")]
    [SerializeField] private float rayLength = 5f;
    [SerializeField] private GameObject rigParentPreview;
    [SerializeField] private GameObject rigParentFull;
    [SerializeField] private Material invalidMaterial;
    [SerializeField] private Material validMaterial;

    [SerializeField] private string actionText;

    private ItemHolder itemHolder;
    private GameObject previewObject;
    private Renderer[] previewRendererList;
    private bool wasPreviewCreated = false;
    private bool hasPlacementStarted = false;
    private bool canRigBePlaced = false;
    private bool wasPlacementAccepted = false;

    private Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    private Vector3 placementPosition;

    public static Action<bool> RigPlaced; //used for RigHighlight disabling

    private void OnEnable()
    {
        /* Subscribes to event(s). */
        //ActivateRig.onActivateRig += OpenRig;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        //ActivateRig.onActivateRig -= OpenRig;
    }

    private void Start()
    {
        itemHolder = GetComponentInParent<ItemHolder>();
    }

    private void Update()
    {
        if (!wasPlacementAccepted)
        {
            ShowPlacement();
        }        
    }

    private void ShowPlacement()
    {
        if (hasPlacementStarted)
        {
            Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

            if (!wasPreviewCreated)
            {
                CreateRigPreview();
            }

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
            {
                if (hit.collider.gameObject.CompareTag("Metal"))
                {
                    previewObject.SetActive(true);
                    previewObject.transform.position = hit.point;
                    foreach (Renderer renderer in previewRendererList) { renderer.material = invalidMaterial; }
                    canRigBePlaced = false;
                    placementPosition = hit.point;
                }
                else if (hit.collider.gameObject.CompareTag("RigAdapter"))
                {
                    RigPlacement rigAdapter = hit.collider.gameObject.GetComponent<RigPlacement>();
                    
                    previewObject.SetActive(true);
                    previewObject.transform.position = rigAdapter.RigPlacementPosition.position;
                    previewObject.transform.rotation = Quaternion.Euler(rigAdapter.RigPlacementRotation.localEulerAngles);
                    foreach (Renderer renderer in previewRendererList) { renderer.material = validMaterial; }
                    canRigBePlaced = true;
                    placementPosition = rigAdapter.RigPlacementPosition.position;
                }
            }
            else
            {
                if (previewObject != null) { previewObject.SetActive(false); }
            }
        }
    }

    public ItemData ItemData { get { return rigData; } set { rigData = value; } }

    public void Interact()
    {
        if (pickable) { Destroy(rigParentTransform.gameObject); }
    }

    public void Use()
    {
        if (!hasPlacementStarted)
        {
            hasPlacementStarted = true;
        }
        else if (!wasPlacementAccepted)
        {
            PlaceRig();
        }
    }

    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }

    private void CreateRigPreview()
    {
        GameObject preview = Instantiate(rigParentPreview);
        preview.gameObject.name = "RigPreview";
        preview.transform.localRotation = Quaternion.identity;
        previewObject = preview;
        previewRendererList = previewObject.GetComponentsInChildren<Renderer>();
        wasPreviewCreated = true;
    }

    private void PlaceRig()
    {
        if (canRigBePlaced)
        {
            /* Apply position and rotation. */
            GameObject rigObject = Instantiate(rigParentFull);
            rigObject.gameObject.name = "Rig";
            rigObject.transform.position = previewObject.transform.position;
            rigObject.transform.localRotation = Quaternion.Euler(previewObject.transform.localEulerAngles);

            /* Delete rigParentPreview from scene */
            Destroy(previewObject);

            /* Unchild from ItemHolder and remove from itemList. */
            itemHolder.RemoveFromInventory(rigData);

            RigPlaced?.Invoke(true);

            Destroy(gameObject);
        }
    }

    public void ResetControls()
    {
        wasPreviewCreated = false;
        Destroy(previewObject);
        hasPlacementStarted = false;
        wasPlacementAccepted = false;
    }
}
