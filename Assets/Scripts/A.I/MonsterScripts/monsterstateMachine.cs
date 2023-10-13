using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterStateMachine : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private AnomalyDirector director;

    [Header("Monster Settings")]
    [SerializeField] public AnomalyState currentState;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] public float chaseSpeed = 3f;
    [SerializeField] public float fieldOfView = 60f;
    [SerializeField] public float viewDistance = 10f;
    [SerializeField] public float maxKillRange = 5f;
    [SerializeField] public float coneAngle = 45f;
    [SerializeField] public float maxDetectDistance = 10f;
    [SerializeField] private bool playerDetected = false;

    public enum AnomalyState
    {
        Patrol,
        Hunt,
        Stalk,
        Chase,
        Kill,
    }

    private Vector3 targetDestination;

    void Start()
    {
        currentState = AnomalyState.Patrol;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckForPlayer();

        switch(director.CurrentDirectorState)
        {
            case AnomalyDirector.DirectorState.Search:
                
                Patrol();
                break;

            case AnomalyDirector.DirectorState.Hunt:

                switch (currentState)
                {
                    case AnomalyState.Stalk:
                        Stalk();
                        break;
                    case AnomalyState.Chase:
                        Chase();
                        break;
                    case AnomalyState.Kill:
                        Kill();
                        break;
                }

                break;
        }  
    }

    /*
     * During the Search State (Director), the Anomaly object will travel to their target
     * position provided and managed in the anamolyScript. If the player is detected at any point,
     * the Anomaly object will change the Director's state, change it's own state, advise the Director
     * that it is moving (prevents Director from constantly assigning a target destination). If the player
     * is not found, will calculate the distance between the target position and its position to advise 
     * the Director that it's not moving and can assign a new target position.
     */
    void Patrol()
    {
        agent.SetDestination(targetDestination);

        if (playerDetected)
        {
            director.CurrentDirectorState = AnomalyDirector.DirectorState.Hunt;
            currentState = AnomalyState.Chase;
            director.IsAnomalyMoving = true;
        }
        else if (Vector3.Distance(transform.position, targetDestination) < 2)
        {
            director.IsAnomalyMoving = false;
        }
    }

    /* Only during Hunt State (Director) <Look at Patrol comment> */
    void Stalk()
    {
        agent.SetDestination(targetDestination);

        if (playerDetected)
        {
            director.IsAnomalyMoving = true;
            currentState = AnomalyState.Chase;
        }
        else if (Vector3.Distance(transform.position, targetDestination) < 2)
        {
            director.IsAnomalyMoving = false;
        }
    }

    /* Kills the player if they are in range (determined by Chase State) */
    void Kill()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = director.PlayerPosition - transform.position;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, maxKillRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                KillPlayer();
            }
        }
    }

    /*
     * Chases the player if they are within range of detection. If true, chase player. If true and player
     * is within kill range, change state to kill. If false, clear (delete all elements) from the queue 
     * (prevents Anomaly object from going to previous positions after detection), adds the most recent
     * position of the player, advises the Director that it's not moving (to assign a new target position),
     * and changes state to Stalk.
     */
    void Chase()
    {
        if (Vector3.Distance(transform.position, director.PlayerPosition) < maxDetectDistance)
        {
            Debug.Log("IN");
            agent.SetDestination(director.PlayerPosition);

            if (Vector3.Distance(transform.position, director.PlayerPosition) < maxKillRange)
            {
                currentState = AnomalyState.Kill;
            }
        }
        else
        {
            Debug.Log("OUT");
            director.HuntNodesQueue.Clear();
            director.HuntNodesQueue.Enqueue(director.PlayerPosition);
            director.IsAnomalyMoving = false;
            currentState = AnomalyState.Stalk;
        }
    }

    /* Reset the scene after a short delay (adjust the delay time as needed) */
    private void KillPlayer() { Invoke("ResetScene", 2f); }

    /* Reload the current scene */
    private void ResetScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

    /*
     * Primarily controls the state of the Anomaly object. Constantly runs regardless of state, Director
     * or itself.
     */
    private void CheckForPlayer()
    {
        int numRays = 30;
        float angleIncrement = coneAngle / numRays;
        Vector3 forwardDirection = transform.forward; // Get the forward direction

        for (int i = 0; i < numRays; i++)
        {
            // Calculate the direction of the ray based on angle
            float angle = i * angleIncrement - coneAngle / 2;
            Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * forwardDirection;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, maxDetectDistance, layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerDetected = true;
                    Debug.Log("Player Detected");
                }
                else
                {
                    playerDetected = false;
                    Debug.Log("Player Not Detected");
                }
            }

            // Draw ray for visualization
            Debug.DrawRay(transform.position, direction * maxDetectDistance, Color.blue);
        }

        // Draw a line indicating the player's facing direction
        Debug.DrawRay(transform.position, forwardDirection * maxDetectDistance, Color.green);
    }

    /* Getter/Setters */
    public Vector3 TargetDestination
    {
        get { return targetDestination; }
        set { targetDestination = value; }
    }
}
