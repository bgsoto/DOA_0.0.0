using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wrench : MonoBehaviour
{
    [SerializeField] bool isEquipped;
   
    
    
    private void Start()
    {
        transform.localPosition = new Vector3(-0.204f, -0.88f, 0.202f);
        transform.localRotation = Quaternion.Euler(10.23f, 24.01f, 0f);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void Update()
    {
       if ( transform.parent == true)
        {
            transform.localPosition = new Vector3(-0.204f, -0.88f, 0.202f);
            transform.localRotation = Quaternion.Euler(10.23f, 24.01f, 0f);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
