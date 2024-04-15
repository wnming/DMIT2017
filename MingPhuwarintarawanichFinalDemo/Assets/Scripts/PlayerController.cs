using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;

    [SerializeField] GameObject sword;
    bool isSwingSword;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 35.0f;
        rotationSpeed = 60.0f;
        isSwingSword = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            isSwingSword = true;
        }

        if (isSwingSword)
        {
            sword.transform.localPosition = Vector3.Slerp(sword.transform.localPosition, new Vector3(0, 1, 0), 0.01f);
        }
    }
}
