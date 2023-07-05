using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GadgetOrientation: MonoBehaviour
{
    public Transform Invposition;
    public Vector3 face;

   

    // Update is called once per frame
    void Update()
    {


        transform.position = Invposition.position; ;



    }
}
