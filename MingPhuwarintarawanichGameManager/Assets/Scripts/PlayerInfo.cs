using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public Vector3 spawnLocation;
    public Vector3 currentLocation;
    public string currentScene;
    public int currentHealth;
    public int treasure;

    public float magnitude;
    public Vector3 normalized;
    public Vector3 lastTownPosition;

    public List<TownInfo> townInfoList;
}
