using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int inputNumber;
    private float cycleLength = 2f;
    private bool codeEntered = false;

    public static Action<int> UpdateInput;

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

    private void Rotate()
    {
        gameObject.transform.DOLocalRotate(new Vector3(0, 360, 0), cycleLength, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        });
    }

    void DisableInteract()
    {
        codeEntered = true;
    }//turns off planet interaction upon correct code
}