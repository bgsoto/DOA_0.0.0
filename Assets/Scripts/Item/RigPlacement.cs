using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigPlacement : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private Transform rigParentTransform;

    private Transform rigPlacementTransform;

    private void Start()
    {
        rigPlacementTransform = gameObject.transform;
    }

    public Transform RigPlacementPosition
    {
        get { return rigPlacementTransform; }
        set { rigPlacementTransform = value; }
    }

    public Transform RigPlacementRotation
    {
        get { return rigParentTransform; }
        set { rigParentTransform = value; }
    }
}
