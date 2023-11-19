using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public int moneyCount;
    public int playerExp;

    public List<Intel> intelCollected;

    public PlayerStats()
    { //sets default values
        this.playerName = "DefaultName";
        this.moneyCount = 0;
        this.playerExp = 0;

        this.intelCollected = new List<Intel>();
    }
}