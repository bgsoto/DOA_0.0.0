using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private int inputNumber;
    [SerializeField] private float cycleLength = 2f;
    [SerializeField] private bool codeEntered = false;
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    public static Action<int> UpdateInput;

    /* Not used */
    private ItemData itemData;

    private void OnEnable()
    {
        PlanetsManager.planetCorrect += DisableInteract;
    }

    private void OnDisable()
    {
        PlanetsManager.planetCorrect -= DisableInteract;
    }

    public void Interact()
    {
        if (codeEntered) { return; }
        UpdateInput?.Invoke(inputNumber);
        Rotate();
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    public string ActionText { get { return actionText; } set { actionText = value; } }

    private void Rotate()
    {
        gameObject.transform.DOLocalRotate(new Vector3(0, 360, 0), cycleLength, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        });
    }

    void DisableInteract()
    {
        // Turns off planet interaction upon correct code
        codeEntered = true;
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
}