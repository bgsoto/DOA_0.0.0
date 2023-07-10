using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour


{
    public GameObject point, spot;
    public bool on;
    //[SerializeField] Flashlight flashlightscript;



    private void Start()
    {
       
        on = false;
    }
    public void Update()
    {
        
        // collects mouse input to turn light off/on
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (on == false) 
            {

                on = true;
                TurnOn();
            
            
            }

            else
            {

                on = false;
                TurnOff();
            }
            
        }

    }



    




    public void TurnOn()
    {
        point.SetActive(true);
        spot.SetActive(true);
    }

    public void TurnOff()
    {
        point.SetActive(false);
        spot.SetActive(false);
    }

    
}
