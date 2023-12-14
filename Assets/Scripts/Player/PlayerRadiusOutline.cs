using EPOOutline;
using Unity.Netcode;
using UnityEngine;

public class PlayerRadiusOutline : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            if (other.gameObject.GetComponent<Outlinable>() != null)
            {
                Outlinable outline = other.gameObject.GetComponent<Outlinable>();
                outline.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner) return;
        if (other.gameObject.GetComponent<Outlinable>() != null)
        {
            Outlinable outline = other.gameObject.GetComponent<Outlinable>();
            outline.enabled = false;
        }
    }
}