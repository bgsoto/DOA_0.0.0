using System;
using System.Collections.Generic;
using UnityEngine;

public class KeycardSpawnManager : MonoBehaviour
{
    [Tooltip("Must be greater than or equal to the amount of cards.")]
    [SerializeField] private List<GameObject> spawnPoints;
    [Tooltip("Cards to spawn on the map this spawn manager is in.")]
    [SerializeField] private List<GameObject> keycardList;

    public static Action<string> cardSpawned;
    private void Start()
    {
        if (spawnPoints == null) { return; }
        foreach (GameObject card in keycardList)
        {
            if (spawnPoints == null || spawnPoints.Count <= 0) { return; }
            //pick a random spawn, spawn intel, remove spawn to prevent duplicate spawns.
            var spawnpoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
            Instantiate(card, spawnpoint.transform);
            cardSpawned?.Invoke(spawnpoint.name);
            spawnPoints.Remove(spawnpoint);
        }
    }
}