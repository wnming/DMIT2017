using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 25.0f;
    }

    private void FixedUpdate()
    {
        rigidbody.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * movementSpeed);
        rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);
    }
}
