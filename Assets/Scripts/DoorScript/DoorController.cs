using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [System.Serializable]
    public class Door
    {
        public Animator leftDoorAnim;
        public Animator rightDoorAnim;
        public DoorTrigger doorTrigger; // Reference to the DoorTrigger script
    }

    public List<Door> doors; // List of doors that you can populate from the Unity Editor

    public void DoorTriggered(DoorTrigger triggeredDoorTrigger, Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            Debug.Log("Entity entered the trigger");
            Door triggeredDoor = GetDoorFromTrigger(triggeredDoorTrigger);
            if (triggeredDoor != null)
            {
                StartCoroutine(OpenAndCloseDoors(triggeredDoor));
            }
        }
    }

    private Door GetDoorFromTrigger(DoorTrigger doorTrigger)
    {
        foreach (Door door in doors)
        {
            if (door.doorTrigger == doorTrigger)
            {
                return door;
            }
        }
        return null;
    }

    private IEnumerator OpenAndCloseDoors(Door door)
    {
        Debug.Log("Attempting to open door");
        door.leftDoorAnim.SetTrigger("OpenLeft");
        door.rightDoorAnim.SetTrigger("OpenRight");

        yield return new WaitForSeconds(3); // Wait for 3 seconds as per your requirement

        Debug.Log("Attempting to close door");
        door.leftDoorAnim.SetTrigger("CloseLeft");
        door.rightDoorAnim.SetTrigger("CloseRight");
    }
}
