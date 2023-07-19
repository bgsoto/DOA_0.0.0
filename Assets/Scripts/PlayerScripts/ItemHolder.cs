using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{

    [SerializeField] Transform wrenchPos;
    [SerializeField] Transform tabletPos;
    [SerializeField] Transform jerrycanPos;
    [SerializeField] Transform flashlightPos;

   void FlashLight()

    {

        flashlightPos.position = new Vector3(0.03f,-0.44f,-0.1f);
        flashlightPos.rotation = Quaternion.Euler(19.66f,40.06f,5f);

    }
    void Wrench()

    {

        wrenchPos.position = new Vector3(0.03f, -0.44f, -0.1f);
        wrenchPos.rotation = Quaternion.Euler(19.66f, 40.06f, 5f);

    }
    void Tablet()

    {

        tabletPos.position = new Vector3(0.091f, -0.086f, 0.178f);
        tabletPos.rotation = Quaternion.Euler(-20.4f, 174.5f, -93.2f);

    }
    void Jerrycan()

    {

        jerrycanPos.position = new Vector3(0.104f, -0.395f, 0.096f);
        jerrycanPos.rotation = Quaternion.Euler(4.83f,107.66f,2.5f);

    }

}
