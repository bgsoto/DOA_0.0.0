using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool canInteract = false;
    private IInteractable interactableObject;
    
    public void OnTriggerEnter(Collider collision)
    {
        /* Enables the player to interact with an object that has the IInteractable interface. */
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            interactableObject = collision.gameObject.GetComponent<IInteractable>();
            canInteract = true;
            Debug.Log("PRESS F TO INTERACT");
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            canInteract = false;
            Debug.Log("NO ACTION AVAILIABLE");
        }
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                interactableObject.Interact();
            }
        }
    }
}
