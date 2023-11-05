using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTablet : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private Animator rigAnimator;
    [SerializeField] private ShowKeypad tablet;

    private void OnEnable()
    {
        ActivateDetector.onPluggedRig += OpenRig;
    }

    private void OnDisable()
    {
        ActivateDetector.onPluggedRig -= OpenRig;
    }

    public void OpenRig()
    {
        rigAnimator.SetTrigger("Open");
    }

    public void EnableTablet() { tablet.CanInteract = true; }
}
