using UnityEngine;
using UnityEngine.SceneManagement;

public class AnomalyDetection : MonoBehaviour
{
    [SerializeField] private string targetObjectTag;
    [SerializeField] private float distanceToContain;
    
    private Transform targetObjectTransform;

    private bool isDetected = false;
    private bool isTrapOn = false;

    private void OnEnable()
    {
        /* Subscribes to event(s). */
        KeypadDisplayManager.OnExitKeypad += EnableTrap;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        KeypadDisplayManager.OnExitKeypad -= EnableTrap;
    }

    /* Detects if the Anomaly object is in within the containment area. */
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(targetObjectTag))
        {
            isDetected = true;
            targetObjectTransform = collider.gameObject.transform;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(targetObjectTag))
        {
            isDetected = false;
        }
    }

    private void Update()
    {
        /* 
         * If the Anomaly object is within the containment area and the Rig is enabled,
         * do something to the Anomaly.
         * 
         * Gets the distance from the center of the capture area and the Anomaly object to ensure
         * the Anomaly object is completely inside the containment area.
         */
        if (isDetected) 
        {
            float distance = Vector3.Distance(transform.position, targetObjectTransform.position);

            if (distance <= distanceToContain && isTrapOn)
            {
                Invoke("ResetScene", 2f);
            }
        }
    }

    public void EnableTrap(bool value) { isTrapOn = value; }

    /* Reload the current scene */
    private void ResetScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
