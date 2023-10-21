using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusSensor : MonoBehaviour
{
    public float radius;
    public LayerMask target;
    public bool canSesnsePlayer;

    public void Update()
    {
       

       // Collider[] hitcolliders = Physics.OverlapSphere(transform.position, radius, target);
       // Gizmos.color = Color.yellow;
       // Gizmos.DrawSphere(transform.position, radius);
       
        
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
       
        Gizmos.DrawWireSphere(transform.position, radius);
    }





}
