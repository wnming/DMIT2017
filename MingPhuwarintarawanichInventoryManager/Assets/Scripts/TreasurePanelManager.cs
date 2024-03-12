using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePanelManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SelectTreasure(int index)
    {
        Debug.Log("select container: " + DataManager.currentContainer + " index: " + index);
    }
}
