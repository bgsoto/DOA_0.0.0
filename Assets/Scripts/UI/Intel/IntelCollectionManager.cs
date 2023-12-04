using System.Collections.Generic;
using UnityEngine;

public class IntelCollectionManager : MonoBehaviour, IDataPersistence
{
    //manages the list of collected intel, call on awake then any time a match ends.
   [SerializeField] public static List<Intel> collectedIntel = new();
    public void LoadData(PlayerStats data)
    {
        collectedIntel = new List<Intel>();
        foreach(Intel intel in data.intelCollected)
        {
            collectedIntel.Add(intel);
        }
    }

    public void SaveData(ref PlayerStats data)
    {
        data.intelCollected.Clear();
        data.intelCollected = collectedIntel;
        Debug.Log(collectedIntel);
    }
}
