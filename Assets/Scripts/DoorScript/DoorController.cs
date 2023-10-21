using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator leftDoorAnim;
    public Animator rightDoorAnim;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
        if (other.CompareTag("Player")) // Ensure your player's tag is set to "Player"
        {
            Debug.Log("Player entered the trigger");
            StartCoroutine(OpenAndCloseDoors());
        }
    }

    private IEnumerator OpenAndCloseDoors()
    {
        Debug.Log("Attempting to open doors");
        leftDoorAnim.SetTrigger("OpenLeft");
        rightDoorAnim.SetTrigger("OpenRight");
        
        yield return new WaitForSeconds(4);

        Debug.Log("Attempting to close doors");
        leftDoorAnim.SetTrigger("CloseLeft");
        rightDoorAnim.SetTrigger("CloseRight");
    }
}
