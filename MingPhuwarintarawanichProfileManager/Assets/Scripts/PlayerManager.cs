using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject shark;
    [SerializeField] Material[] colors;

    [SerializeField] GameObject[] sharkProps;

    [SerializeField] TextMeshProUGUI time;
    float timeT;


    [SerializeField] GameObject overwritePanel;
    [SerializeField] GameObject leaderboardPanel;
    [SerializeField] TextMeshProUGUI leaderboardText;

    public float horizontalInput;
    public float verticalInput;
    float moveSideToSideSpeed = 15.0f;
    //float moveTowardSpeed = 15.0f;
    float initialMoveTowardSpeed = 5.0f;
    float moveTowardSpeed;
    float maxMoveTowardSpeed = 15.0f;
    Rigidbody rb;

    private bool isSpeedBoostActive;
    int speedBoostTime = 2;
    int boostTimes = 0;
    private bool isObstacleActive;
    bool isFinish;
    bool isGameSave;

    List<GhostPositionData> tempGhostPosition;

    // Start is called before the first frame update
    void Start()
    {
        moveTowardSpeed = initialMoveTowardSpeed;
        overwritePanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        isSpeedBoostActive = false;
        isObstacleActive = false;
        isFinish = false;
        isGameSave = false;
        rb = GetComponent<Rigidbody>();
        tempGhostPosition = new List<GhostPositionData>();
        DisabledProps();
        gameObject.GetComponent<MeshFilter>().sharedMesh = shark.GetComponent<MeshFilter>().sharedMesh;
        if (DataManager.allProfiles[DataManager.currentProfile].vehicleTypeIndex == 1)
        {
            //prince
            sharkProps[0].SetActive(true);
        }
        else 
        {
            if(DataManager.allProfiles[DataManager.currentProfile].vehicleTypeIndex == 2)
            {
                //sword
                sharkProps[1].SetActive(true);
            }
        }
        gameObject.GetComponent<Renderer>().material = colors[DataManager.allProfiles[DataManager.currentProfile].vehicleColorIndex];
    }

    void DisabledProps()
    {
        for(int index = 0; index < sharkProps.Length; index++)
        {
            sharkProps[index].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinish)
        {
            time.text = $"Time: {timeT += Time.deltaTime}";
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (!isSpeedBoostActive) 
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if(moveTowardSpeed < maxMoveTowardSpeed)
                    {
                        moveTowardSpeed += 5.0f;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    moveTowardSpeed = initialMoveTowardSpeed;
                }
            }
            GhostPositionData positionData = new GhostPositionData();
            positionData.x = gameObject.transform.position.x;
            positionData.y = gameObject.transform.position.y;
            positionData.z = gameObject.transform.position.z;
            tempGhostPosition.Add(positionData);
        }
        else
        {
            if(DataManager.allProfiles[DataManager.currentProfile].highscore.Count == 0 || timeT <= DataManager.allProfiles[DataManager.currentProfile].highscore[0])
            {
                if (!isGameSave)
                {
                    isGameSave = true;
                    overwritePanel.SetActive(true);
                }
            }
            else
            {
                if (!isGameSave)
                {
                    isGameSave = true;
                    //CheckTopFiveTimes();
                    //DataManager.SaveData();
                    UpdateLeaderboard();
                }
            }
        }
    }

    void UpdateLeaderboard()
    {
        for (int index = 0; index < DataManager.allProfiles[DataManager.currentProfile].highscore.Count; index++)
        {
            leaderboardText.text += $"{DataManager.allProfiles[DataManager.currentProfile].highscore[index]}\n";
        }
        leaderboardPanel.SetActive(true);
    }

    void CheckTopFiveTimes()
    {
        DataManager.allProfiles[DataManager.currentProfile].highscore.Add(timeT);
        DataManager.allProfiles[DataManager.currentProfile].highscore = DataManager.allProfiles[DataManager.currentProfile].highscore.OrderBy(x => x == 0).ThenBy(x => x).Take(5).ToList();
    }

    public void SaveGhostData()
    {
        CheckTopFiveTimes();
        DataManager.allProfiles[DataManager.currentProfile].ghostDatas.ghostVehicleTypeIndex = DataManager.allProfiles[DataManager.currentProfile].vehicleTypeIndex;
        DataManager.allProfiles[DataManager.currentProfile].ghostDatas.ghostPositionDatas = tempGhostPosition;
        //DataManager.allProfiles[DataManager.currentProfile].highscore = timeT;
        DataManager.SaveData();
        overwritePanel.SetActive(false);
        UpdateLeaderboard();
    }

    public void RejectSaveGhostData()
    {
        overwritePanel.SetActive(false);
        UpdateLeaderboard();
    }

    private void FixedUpdate()
    {
        if (!isFinish)
        {
            rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Horizontal") * moveSideToSideSpeed);
            rb.AddRelativeForce(Vector3.left * moveTowardSpeed);
        }
    }

    IEnumerator SpeedBoostActivation()
    {
        moveTowardSpeed = maxMoveTowardSpeed + 10.0f;
        isSpeedBoostActive = true;
        while (boostTimes > 0)
        {
            boostTimes -= 1;
            yield return new WaitForSeconds(speedBoostTime);
        }
        isSpeedBoostActive = false;
        moveTowardSpeed = initialMoveTowardSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SpeedBoost")
        {
            collision.gameObject.SetActive(false);
            boostTimes += 1;
            if (!isSpeedBoostActive)
            {
                StartCoroutine(SpeedBoostActivation());
            }
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.SetActive(false);
            moveTowardSpeed = initialMoveTowardSpeed;
            if(maxMoveTowardSpeed > moveSideToSideSpeed)
            {
                maxMoveTowardSpeed -= 5.0f;
            }
        }
        if (collision.gameObject.tag == "Finish")
        {
            isFinish = true;
        }
    }
}
