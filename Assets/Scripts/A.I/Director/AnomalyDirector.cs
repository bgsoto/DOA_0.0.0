using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;
using System.Linq;

public class AnomalyDirector : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private MonsterStateMachine anomaly;
    [SerializeField] private Transform playerTransform;
    

    [Header("Patrol Position(s)")]
    [SerializeField] private List<Transform> patrolNodesList;

    [Header("Director Settings")]
    [SerializeField] private float maxPinPointTimer;

    [Header("Camera Controls")]
    [SerializeField] private GameObject jumpScareCamHolder;
    [SerializeField] private GameObject playerCamHolder;
    public enum DirectorState
    {
        Search,
        Hunt
    }

    [SerializeField] private DirectorState currentState;

    private Queue<Vector3> huntNodesQueue = new Queue<Vector3>();
    private float pinPointTimer;
    
    /*
     * Controls the assignment of the target position. Prevents the target position
     * from being re-assigned infinitely during runtime.
     */
    private bool isAnomalyMoving = false;
    
    void Awake()
    {
        currentState = DirectorState.Search;
    }

    void Update()
    {
        switch (currentState)
        {
            case DirectorState.Search:
                Search();
                break;
            case DirectorState.Hunt:
                Hunt();
                break;
        }
        PlayJumpScare();
    }

    /*
     * During Search State, randomly assigns the Anomaly's target position by generating
     * a random number between 0 and the length of the list. Retrieves a transfrom's 
     * position by accessing the list with the random number and assigns it to the target
     * destination accessible by the Monster's state machine.
     * 
     * Sets the Anomaly object as moving to prevent the variable from updating every frame.
     */
    private void Search()
    {
        if (!isAnomalyMoving)
        {
            int nodeIndex = UnityEngine.Random.Range(0, patrolNodesList.Count);
            anomaly.TargetDestination = patrolNodesList[nodeIndex].position;
            isAnomalyMoving = true;
           
        }

        if (anomaly.playerWasHeard)
        {
            anomaly.TargetDestination = playerTransform.position;
            anomaly.playerWasHeard = false;
        }

    }

    /*
     * During Hunt State, assigns the Anomaly's target position through a queue and 
     * a list of transforms. If the Anomaly is not moving, it checks if the queue is empty. If false,
     * removes and returns the first element in the queue and assigns it. If true, assigns a random
     * transform's position like the Search function.
     * 
     * Sets the Anomaly object as moving to prevent the variable from updating every frame.
     */
    private void Hunt()
    {
        PinPointPlayer();

        if (!isAnomalyMoving)
        {
            if (huntNodesQueue.Count != 0)
            {
                anomaly.TargetDestination = huntNodesQueue.Dequeue();
            }
            else
            {
                int nodeIndex = UnityEngine.Random.Range(0, patrolNodesList.Count);
                anomaly.TargetDestination = patrolNodesList[nodeIndex].position;
            }

            isAnomalyMoving = true;
        }
    }

    /*
     * During Hunt State, constantly pings the player's location in a set amount of time and
     * adds it to the back of the queue. If the queue is larger than 5 elements, removes and 
     * returns the first element, adds the most recent ping to the back of the queue, and 
     * resets the timer.
     */
    public void PinPointPlayer()
    {
        pinPointTimer += Time.deltaTime;

        if (pinPointTimer >= maxPinPointTimer)
        {
            if (huntNodesQueue.Count > 5)
            {
                huntNodesQueue.Dequeue();
                huntNodesQueue.Enqueue(playerTransform.position);
            }
            else
            {
                huntNodesQueue.Enqueue(playerTransform.position);
            }

            pinPointTimer = 0f;
            Debug.Log(huntNodesQueue.Count);
        }
    }


    public void PlayJumpScare()
    {
        if (anomaly.currentState == MonsterStateMachine.AnomalyState.Kill)
        {
            jumpScareCamHolder.SetActive(true);
        }
    }




    /* Getter/Setters */
    public DirectorState CurrentDirectorState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public Queue<Vector3> HuntNodesQueue
    {
        get { return huntNodesQueue; }
    }

    public bool IsAnomalyMoving
    {
        get { return isAnomalyMoving; }
        set { isAnomalyMoving = value; }
    }

    public Vector3 PlayerPosition
    {
        get { return playerTransform.position; }
    }
}
