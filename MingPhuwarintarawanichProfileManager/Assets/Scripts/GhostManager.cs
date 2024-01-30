using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    [SerializeField] GameObject shark;
    [SerializeField] GameObject[] sharkProps;
    [SerializeField] Material transparentMat;

    private int ghostPositionIndex;

    Rigidbody rb;

    private bool isGhostDataExist = false;
    private bool isStop = false;
    private List<GhostPositionData> ghostPositionDatas;

    void Start()
    {
        DisabledProps();
        if (DataManager.allProfiles[DataManager.currentProfile].ghostDatas.ghostPositionDatas.Count > 0) 
        {
            ghostPositionDatas = DataManager.allProfiles[DataManager.currentProfile].ghostDatas.ghostPositionDatas;
            isGhostDataExist = true;
            rb = GetComponent<Rigidbody>();
            gameObject.GetComponent<MeshFilter>().sharedMesh = shark.GetComponent<MeshFilter>().sharedMesh;
            if (DataManager.allProfiles[DataManager.currentProfile].ghostDatas.ghostVehicleTypeIndex == 1)
            {
                //prince
                sharkProps[0].SetActive(true);
            }
            else
            {
                if (DataManager.allProfiles[DataManager.currentProfile].ghostDatas.ghostVehicleTypeIndex == 2)
                {
                    //sword
                    sharkProps[1].SetActive(true);
                }
            }
            gameObject.GetComponent<Renderer>().material = transparentMat;
        }
    }

    void DisabledProps()
    {
        for (int index = 0; index < sharkProps.Length; index++)
        {
            Material[] mats = sharkProps[index].GetComponent<Renderer>().materials;
            for(int matsIndex = 0; matsIndex < mats.Length; matsIndex++)
            {
                mats[matsIndex] = transparentMat;
            }
            sharkProps[index].GetComponent<Renderer>().materials = mats;
            sharkProps[index].SetActive(false);
        }
    }

    void Update()
    {
        if (isGhostDataExist)
        {
            if(ghostPositionIndex < ghostPositionDatas.Count)
            {
                rb.MovePosition(new Vector3(
                    ghostPositionDatas[ghostPositionIndex].x, 
                    ghostPositionDatas[ghostPositionIndex].y, 
                    ghostPositionDatas[ghostPositionIndex].z));
                ghostPositionIndex += 1;
            }
        }
    }
}
