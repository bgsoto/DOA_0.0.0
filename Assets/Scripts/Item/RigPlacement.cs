using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigPlacement : MonoBehaviour
{
    [Header("Rotation of the Rig When Placed")]
    [SerializeField] private Vector3 rigPlacementRotation;

    private Vector3 rigPlacementPostion;

    private void Start()
    {
        rigPlacementPostion = transform.GetComponentInParent<Transform>().position;
    }

    public Vector3 RigPlacementPosition
    {
        get { return rigPlacementPostion; }
        set { rigPlacementPostion = value; }
    }

    public Vector3 RigPlacementRotation
    {
        get { return rigPlacementRotation; }
        set { rigPlacementRotation = value; }
    }
}
