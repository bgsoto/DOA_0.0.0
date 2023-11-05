using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private Animator animator;
    [SerializeField] private MonsterStateMachine Anamoly;
   // [SerializeField] private






    // Start is called before the first frame update
    void Start()
    {



    }

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


    }
}
