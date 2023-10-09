using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public Transform playerLocation;
    public void Awake()
    {
        playerLocation = this.transform;
    }

    public Transform Getter
    {

        get { return playerLocation; }

        set { playerLocation = value; }
 
    }



}
