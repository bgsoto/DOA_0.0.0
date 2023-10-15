using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class ParentDetector : MonoBehaviour
{

    [SerializeField] public Flashlight Flashlightscript;
    [SerializeField] public Tablet tabletScript;
    [SerializeField] public Wrench wrenchScript;
    [SerializeField] public JerryCan jerrycanScript;
    
    
  
    [SerializeField] public List<MonoBehaviour> scriptList;


    private void Start()
    {
       
       scriptList = new List<MonoBehaviour>();
        

    }
    private void Update()
    {
        MonoBehaviour[] itemScripts = GetComponentsInChildren<MonoBehaviour>(true);
       

        foreach (MonoBehaviour script in itemScripts)
            {
                script.enabled = true;

                if (!script.gameObject.activeSelf ) 
                    
                    {
                            
                      script.enabled = false;
            
                    }

            }

       


    }

    

}

