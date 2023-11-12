using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDetector : MonoBehaviour
{
    [SerializeField] private GameObject detectorObject;

    public static Action onPluggedRig;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Rig"))
        {
            detectorObject.SetActive(true);

            /* Subscription: ActivateTablet */
            onPluggedRig?.Invoke();
        }
    }
}
