using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelSpawnManager : MonoBehaviour
{
    [Tooltip("Must be greater than or equal to the amount of intel.")]
   [SerializeField] private List<GameObject> spawnPoints;
    [Tooltip("Intel to spawn on the map this spawn manager is in.")]
   [SerializeField] private List<Intel> intelList;

    private void Start()
    {
        if (spawnPoints == null) { return; }
        foreach (Intel intel in intelList)
        {
            if (spawnPoints == null || spawnPoints.Count <= 0) { return; }
            //pick a random spawn, spawn intel, remove spawn to prevent duplicate spawns.
            var spawnpoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            IntelInteract II = spawnpoint.GetComponent<IntelInteract>();
            II.intel = intel;
            spawnpoint.SetActive(true);
            spawnPoints.Remove(spawnpoint);
        }
    }
}
