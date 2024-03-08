using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabletMap_Player_Indicator : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private Transform targettoFollow;

    [Header("Settings")]
    [SerializeField] private Vector3 Offset;

    private void Update()
    {
        transform.position = targettoFollow.transform.position + Offset;
    }

}
