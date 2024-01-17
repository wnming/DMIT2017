using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    Rigidbody rb;
    float moveSpeed = 30.0f;
    float rotationSpeed = 80.0f;

    bool isRecorded = false;

    List<PositionData> recordPositionData;

    [SerializeField] GameObject replayObject;

    int index;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        recordPositionData = new List<PositionData>();
        index = 0;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R) && !isRecorded)
        {
            index = 0;
            isRecorded = !isRecorded;
        }
        if (isRecorded) 
        {
            //start recording
            if (horizontalInput != 0 || verticalInput != 0)
            {
                Debug.Log("moving");
                PositionData data = new PositionData();
                data.positionX = rb.position.x;
                data.positionY = rb.position.y;
                data.positionZ = rb.position.z;

                data.rotationX = rb.rotation.x;
                data.rotationY = rb.rotation.y;
                data.rotationZ = rb.rotation.z;
                recordPositionData.Add(data);
            }
        }
        else
        {
            //replay the record
            replayObject.transform.position = new Vector3(recordPositionData[index].positionX, recordPositionData[index].positionY, recordPositionData[index].positionZ);
            replayObject.transform.rotation = new Quaternion(recordPositionData[index].rotationX, recordPositionData[index].rotationY, recordPositionData[index].rotationZ, 0.0f);
        }
    }

    void FixedUpdate()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        //rb.AddRelativeForce(Vector3.forward * verticalInput * moveSpeed);

        //transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}

public struct PositionData
{
    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;
}