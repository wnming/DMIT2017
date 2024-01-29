using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GhostData
{
    public int ghostVehicleTypeIndex;
    public List<GhostPositionData> ghostPositionDatas;

    public GhostData()
    {
        ghostVehicleTypeIndex = 0;
        ghostPositionDatas = new List<GhostPositionData>();
    }
}
