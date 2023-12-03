using EPOOutline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractOutline : MonoBehaviour
{
    [SerializeField] private Outlinable outline;
   public void OutineOff()
    {
        outline.enabled = false;
    }
}
