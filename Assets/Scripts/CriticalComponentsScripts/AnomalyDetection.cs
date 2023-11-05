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
        KeypadDisplayManager.OnCorrectCoor += EnableTrap;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        KeypadDisplayManager.OnCorrectCoor -= EnableTrap;
    }

    /* Detects if the Anomaly object is in within the containment area. */
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(targetObjectTag))
        {
            isDetected = true;
            targetObjectTransform = collider.gameObject.transform;
            Debug.Log("ANOMALY DETECTED");
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(targetObjectTag))
        {
            isDetected = false;
            Debug.Log("ANOMALY LOST");
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

    public void EnableTrap() { isTrapOn = true; }

    /* Reload the current scene */
    private void ResetScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
