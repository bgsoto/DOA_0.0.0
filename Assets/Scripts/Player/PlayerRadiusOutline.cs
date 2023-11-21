using EPOOutline;
using UnityEngine;

public class PlayerRadiusOutline : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
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
        if (other.gameObject.GetComponent<Outlinable>() != null)
        {
            Outlinable outline = other.gameObject.GetComponent<Outlinable>();
            outline.enabled = false;
        }
    }
}