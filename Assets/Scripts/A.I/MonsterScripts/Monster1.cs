using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class Monster1 : MonoBehaviour
{

    [SerializeField] private Transform location;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject directorHolder;
    [SerializeField] private Transform playerLoc;
    [SerializeField] private GameObject Lure;
   // [SerializeField] private GameObject AnamolyDirector;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Director = GetComponent<anamolyDirector>();
    }

    // Update is called once per frame
    void Update()
    {

        agent.destination = Lure.transform.position;
        
        if (this.transform.position.x  == Lure.transform.position.x && this.transform.position.z == Lure.transform.position.z)
        {

            directorHolder.GetComponent<anamolyDirector>().UpdateInitialLurePosition();

           

        }
        else
        {
            return;
        }
   
       
    }

    
}

  

   


    
