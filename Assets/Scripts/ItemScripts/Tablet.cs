using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0.227f, -0.04f, 0.083f);
        gameObject.transform.localRotation = Quaternion.Euler(-10.8f, -181.17f, -91f);
        gameObject.transform.localScale = new Vector3(1f, 1f, 3f);
    }
}

   

