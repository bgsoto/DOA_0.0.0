using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animation doorAnimation;
    private bool playerInRange = false;

    private void Start()
    {
        doorAnimation = GetComponent<Animation>();
        if (doorAnimation == null)
        {
            Debug.LogError("Animation component not found on this game object!");
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            Debug.Log("Player is in range.");

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F key pressed.");
                doorAnimation.Play("DoorOpen");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered the trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited the trigger zone.");
        }
    }
}
