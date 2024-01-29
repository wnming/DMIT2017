using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileData
{
    //public int profileNo;
    public string name;
    public int vehicleTypeIndex;
    public int vehicleColorIndex;
    public List<float> highscore;
    public GhostData ghostDatas;

    public ProfileData()
    {
        name = "Empty";
        vehicleTypeIndex = 0;
        vehicleColorIndex = 0;
        highscore = new List<float>();
        ghostDatas = new GhostData ();
    }
}
