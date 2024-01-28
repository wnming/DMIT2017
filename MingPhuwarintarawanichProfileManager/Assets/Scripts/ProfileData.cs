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
    public float highscore;

    public ProfileData()
    {
        name = "Empty";
        vehicleTypeIndex = 0;
        vehicleColorIndex = 0;
        highscore = 0;
    }
}
