using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{

    public bool onPerson = false;
    
    private void Update()
    {
        Vector3 newPosition = new Vector3(0f, 0f, 0f);
        SetNewPosition(newPosition);
    }
    void SetNewPosition(Vector3 newPosition)
    {
        if (onPerson == true)
        {
           // Vector3 newPosition = new Vector3(0f, 0f, 0f);
            this.transform.position = newPosition;
        }
    }

}
