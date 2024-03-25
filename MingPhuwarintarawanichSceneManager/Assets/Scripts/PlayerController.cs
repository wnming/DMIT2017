using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;
    GameObject[] gate;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 35.0f;
        rotationSpeed = 70.0f;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Gate")
    //    {
    //        gate = GameObject.FindGameObjectsWithTag("Door");
    //        Debug.Log("OnTriggerStay:" + gate.Length);
    //        for (int index = 0; index < gate.Length; index++)
    //        {
    //            gate[index].GetComponent<MeshRenderer>().enabled = false;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Gate")
    //    {
    //        gate = GameObject.FindGameObjectsWithTag("Door");
    //        Debug.Log("OnTriggerExit:" + gate.Length);
    //        for (int index = 0; index < gate.Length; index++)
    //        {
    //            gate[index].GetComponent<MeshRenderer>().enabled = true;
    //        }
    //    }
    //}

}
