using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class player_Footsteps : MonoBehaviour
{
    /*
     * walking 7.5
     * Running 15
     * crouch walk 2
     * Still 0
     * 
     */
    public bool isCourchWalking;
    public bool isWalking;
    public bool isRunning;
    public bool isMoving;
    public float footsteps;
    public float footStepSoundRadius;
    public void Update()
    {
        walkingSound(this.gameObject.transform.position, footStepSoundRadius);
    }
    public void walkingSound(Vector3 center, float radius)
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders);
        
        for (int i = 0; i < numColliders; i++)
        {
            if (i > 0)
            {
                if (hitColliders[i].CompareTag("Monster")) 
                { 
                    Debug.Log("Gameobject: " + hitColliders[i] + " Heard player at " + transform.position); 
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, footStepSoundRadius);
    }


}


