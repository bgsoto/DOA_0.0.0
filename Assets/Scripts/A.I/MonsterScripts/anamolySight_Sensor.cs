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

    [Header("Anom. Sight Settings")] 
    [SerializeField] public float radius;
    [Range(0,360)]
    [SerializeField] public float angle;
    [SerializeField] public bool canSeePlayer;

    [Header(" Anom. Sense Settings")]
    [SerializeField] public float senseRadius;
    [Range(0, 360)]
    [SerializeField] public float senseAngle;
    [SerializeField] public bool canSensePlayer;

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
            AnomalySenseFOV();
        }
    }

    private void FieldOfViewCheck()
    {
        // Find all colliders within a sphere around the current object
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // If there are any colliders found
        if (rangeChecks.Length != 0)
        {
            // Get the first target's transform
            Transform target = rangeChecks[0].transform;

            // Calculate the normalized direction from current position to target
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Check if the angle between forward direction and direction to target is within the FOV
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // Calculate the distance to the target
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Raycast to check for obstacles in the line of sight
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    canSeePlayer = true; // Player is within FOV and not obstructed by obstacles
                else
                    canSeePlayer = false; // Player is obstructed by obstacles
            }
            else
                canSeePlayer = false; // Player is outside of FOV

        }
        else if (canSeePlayer)
            canSeePlayer = false; // No targets found, reset canSeePlayer flag
    }

    private void AnomalySenseFOV()
    {
        Collider[] SenseCheck = Physics.OverlapSphere(transform.position, senseRadius, targetMask);

        if (SenseCheck.Length != 0)
        {
            canSeePlayer = true;
        }
        else canSeePlayer = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }


}
