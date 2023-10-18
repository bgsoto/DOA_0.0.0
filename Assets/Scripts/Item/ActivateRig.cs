using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRig : MonoBehaviour
{
    public static Action onActivateRig;

    private void OnDestroy()
    {
        onActivateRig?.Invoke();
    }
}
