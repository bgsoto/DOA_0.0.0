using JetBrains.Annotations;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class player_Footsteps : MonoBehaviour
{
   
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    
 
    public float footStepSoundRadius;
    public void Update()
    {
        walkingSoundRadiusController();
        walkingSound(this.gameObject.transform.position, footStepSoundRadius);
    }
    public void walkingSound(Vector3 center, float radius)
    {
        int maxColliders = 20;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders);
        
        for (int i = 0; i < numColliders; i++)
        {
            if (i > 0)
            {
                if (hitColliders[i].CompareTag("Monster")) 
                { 
                    //Debug.Log("EVent played");
                    Sound.onPlayerHeard();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, footStepSoundRadius);
    }

    private void walkingSoundRadiusController()
    {
       if (starterAssetsInputs.move == Vector2.zero) footStepSoundRadius = 0f;

       if (starterAssetsInputs.move != Vector2.zero) footStepSoundRadius = 7.5f;
       // Debug.Log("Walking radius is now 7.5");
        //if ()

        if (starterAssetsInputs.crouch == true && starterAssetsInputs.move != Vector2.zero)
       {
            footStepSoundRadius = 2f;
            //Debug.Log("Your crouching sound decresed to 2");
       }
       if (starterAssetsInputs.sprint == true && starterAssetsInputs.move != Vector2.zero)
        {
            footStepSoundRadius = 15f;
            //Debug.Log("your sprinting your sound is increased by 15");
        }
      
            
    }


}


