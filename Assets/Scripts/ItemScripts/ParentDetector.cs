using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDetector :  MonoBehaviour
{

    public Flashlight Flashlightscript;

    
    // Start is called before the first frame update
    void Start()
    {
        
   
    
    
    }


    private void Update()
    {

        ParentCheck();
    
    
    }

    public void ParentCheck()
    {
        if (transform.parent != null)
        {
            Flashlightscript.enabled = true;

        }
        else
        {
            Flashlightscript.enabled = false;
        }
    }
}
