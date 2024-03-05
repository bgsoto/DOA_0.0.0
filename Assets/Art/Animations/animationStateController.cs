using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private Animator animator;
    [SerializeField] private MonsterStateMachine Anamoly;
    // Update is called once per frame
    void Update()
    {
        if (Anamoly.currentState == MonsterStateMachine.AnomalyState.Patrol || Anamoly.currentState == MonsterStateMachine.AnomalyState.Stalk)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Anamoly.currentState == MonsterStateMachine.AnomalyState.Chase)
        {
            animator.SetBool("isRunning", true);

        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Anamoly.currentState == MonsterStateMachine.AnomalyState.Kill)
        {
            animator.SetBool("playerIsCaptured", true);
        }
        else
        {
            animator.SetBool("playerIsCaptured", false);
        }

        
    }
}
