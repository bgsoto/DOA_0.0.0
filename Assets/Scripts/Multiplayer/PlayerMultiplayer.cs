using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMultiplayer : NetworkBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public override void OnNetworkSpawn()
    {
        Debug.Log("OnNetworkSpawn");
        transform.position = spawnPoint.position;

        if (!IsOwner)
        {
            
        }
    }
    private void Update()
    {
        if (!IsOwner)
        {

        }
    }
}
