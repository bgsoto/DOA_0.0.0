using UnityEngine;
using UnityEngine.SceneManagement;

public class AnomalyDetection : MonoBehaviour
{
    [SerializeField] private string targetObjectTag;
    [SerializeField] private float distanceToContain;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private ParticleSystem containParticles;

    private Transform targetObjectTransform;

    private bool isDetected = false;
    private bool isTrapOn = false;
    private bool particlesOn = false;

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
            Debug.Log("anomaly detected");
            float distance = Vector3.Distance(transform.position, targetObjectTransform.position);

            if (distance <= distanceToContain && isTrapOn)
            {
                Debug.Log("anomaly banished");
                DataPersistenceManager.Instance.SaveGame();
                Invoke("ResetScene", 2f);
                isTrapOn = false;
            }
        }
        if (isTrapOn && particlesOn == false)
        {
            containParticles.Play();
            particlesOn = true;
        }
    }

    public void EnableTrap(bool value) { isTrapOn = value; }

    /* Loads specified scene */
    private void ResetScene() { SceneManager.LoadSceneAsync(sceneToLoad); }
}
