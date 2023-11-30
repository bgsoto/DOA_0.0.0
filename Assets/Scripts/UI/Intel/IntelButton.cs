using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntelButton : MonoBehaviour
{
    public Intel intel;
    public static Action<Intel> intelPressed;

    private void OnEnable()
    {
        IntelMonitorManger.inIntelMenu += UpdateInteract;
    }
    private void OnDisable()
    {
        IntelMonitorManger.inIntelMenu -= UpdateInteract;
    }
    public void OnPressed()
    {
        if(intel != null)
        {
            intelPressed?.Invoke(intel);
        }
    }
    void UpdateInteract(bool value)
    {
        if(IntelCollectionManager.collectedIntel.Contains(intel))
        {
            this.GetComponent<Button>().interactable = true;
            this.GetComponentInChildren<TMP_Text>().text = intel.intelTitle;
        }
    }
}