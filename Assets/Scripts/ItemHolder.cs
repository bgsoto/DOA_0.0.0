using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Irotate;


    private void Update()
    {
        
        transform.rotation = Irotate.rotation;


    }
}
