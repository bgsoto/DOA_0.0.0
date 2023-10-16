using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracker_itemScript : MonoBehaviour
{
    
    [SerializeField] private bool isEquipped;

    private void Start()
    {
        
    }
    void Update()
    {
        checkIfEquipped();
        

    }
    void equippedPosition()
    {
        if (isEquipped)
        {
            this.gameObject.transform.localPosition = new Vector3(0.167f, -0.125f, 0.244f);
            this.gameObject.transform.localRotation = Quaternion.Euler(-73.589f, 14.065f, -15.101f);
        }
    }

    void checkIfEquipped()
    {
        if (transform.parent != null)
        {
            isEquipped = true;
            equippedPosition();

        }
        else
        {
            isEquipped = false;
        }
    }

    void bringToFace()
    {

    }
}
