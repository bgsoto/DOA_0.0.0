using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;






public class EnemyStateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Stalk,
        Kill,
        Chase,
        DetectPlayer,
        LostSight
    }

    [SerializeField] public State currentState;
    [SerializeField] public Transform[] patrolPoints;
    //[SerializeField] private int currentPatrolPointIndex = 0;
    [SerializeField] public Transform player;
    [SerializeField] public float chaseSpeed = 3f;
    [SerializeField] public float fieldOfView = 60f;
    [SerializeField] public float viewDistance = 10f;
    [SerializeField] public float maxKillRange = 5f;
    [SerializeField] public float coneAngle = 45f;
    [SerializeField] public float maxDetectDistance = 10f;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float timeSearching = 5f;
    [SerializeField] private float searchTimer = 0f;
    [SerializeField] private bool playerDetected = false;

    void Start()
    {
        currentState = State.Patrol;
        Shuffle(patrolPoints);

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();               
                break;
            case State.Stalk:
                Stalk();
                break;
            case State.Kill:
                Kill();
                break;
            case State.Chase:
                Chase();
                break;
            case State.LostSight:
                LostSight();
                break;
            default:
                break;
        }
    }

    void Patrol()
    {


        // Check if the AI is not currently calculating a path and is very close to the current destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("if happened");
            // Generate a random index to select a new patrol point
            int randomPointIndex = Random.Range(0, patrolPoints.Length);

            // Set the AI's destination to the position of the randomly selected patrol point
            agent.SetDestination(patrolPoints[randomPointIndex].position);
        }
        
        

        // Check if the player is in the AI's line of sight
        CheckForPlayer();



    }

    void Stalk()
    {
        Debug.Log("Stalking...");
        // Add your stalking behavior
    }

    void Kill()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, maxKillRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                KillPlayer();
            }
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) < maxKillRange)
        {
            ChangeState(State.Kill);
        }

        CheckForPlayer();
        // If player is not detected, start the search timer
        if (!playerDetected)
        {
            searchTimer += Time.deltaTime;

            if (searchTimer >= timeSearching)
            {
                ChangeState(State.LostSight); // Transition to LostSight state
                searchTimer = 0f; // Reset the timer
            }
        }
        else
        {
            searchTimer = 0f; // Reset the timer if player is detected
        }

    }

   

    public void ChangeState(State newState)
    {
        currentState = newState;

        if (newState == State.Chase)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Shuffle(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    void KillPlayer()
    {
       

        // Reset the scene after a short delay (adjust the delay time as needed)
        Invoke("ResetScene", 2f);
    }

    void ResetScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void CheckForPlayer()
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
                    ChangeState(State.Chase);
                    playerDetected = true;
                    return;
                }

                playerDetected = false;

            }


            // Draw ray for visualization
            Debug.DrawRay(transform.position, direction * maxDetectDistance, Color.blue);
        }

        // Draw a line indicating the player's facing direction
        Debug.DrawRay(transform.position, forwardDirection * maxDetectDistance, Color.green);
    }

    

    void LostSight()
    {
      
           
            ChangeState(State.Patrol);
         
    }



}

