using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryCan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(-0.015f, -0.717f, 0.419f);
        gameObject.transform.localRotation = Quaternion.Euler(4.83f, 107.66f, 2.5f);
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
    }

  
}
