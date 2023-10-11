using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SightCone : MonoBehaviour
{
    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private float maxRange = 10f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;

    void Update()
    {
        Vector3 forward = transform.forward;

        for (float angle = -coneAngle / 2; angle <= coneAngle / 2; angle += 1f)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * forward;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, maxRange, targetLayer))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                Debug.Log("Target detected: " + hit.collider.gameObject.name);
            }
            else if (Physics.Raycast(transform.position, direction, out hit, maxRange, obstacleLayer))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, direction * maxRange, Color.green);
            }
        }
    }
}



