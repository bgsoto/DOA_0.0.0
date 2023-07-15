using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{

    public GameObject point, spot;
    public bool on;

    private void Start()
    {
     
        
        FlashLightPosition();
        on = false;
    }

    public void Update()

    {
       

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

    void FlashLightPosition()

    {
        
            transform.localPosition = new Vector3(0.08f, -0.1f, 0.17f);
            transform.localRotation = Quaternion.Euler(-95f, -86.91f, 0f);
        
    }


}
