using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*                                     Script Reference 
 * -----------------------------------------------------------------------------------------
 * Comp-3 Interactive 
 * "How to Add a Field of View for Your Enemies [Unity Tutorial]"
 * Link: https://www.youtube.com/watch?v=j1-OyLo77ss
 * -----------------------------------------------------------------------------------------
 */

public class anamolySight_Sensor : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] public GameObject playerReference;

    [Header("Settings")] 
    [SerializeField] public float radius;
    [Range(0,360)]
    [SerializeField] public float angle;
    [SerializeField] public bool canSeePlayer;

    [Header("Layer Maskes")]
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public  LayerMask obstacleMask;

    private void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized; 

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    canSeePlayer = true;
               
                else
                    canSeePlayer = false;

            }
            else
                canSeePlayer = false;

        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }


}
