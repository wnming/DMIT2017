using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject details;
    [SerializeField] private GameObject pig;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 25.0f;
        rotationSpeed = 70.0f;
    }

    void Update()
    {
        if (!dialogueBox.activeSelf)
        {
            details.SetActive(false);
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);
        }
        else
        {
            details.SetActive(true);
        }
        if (pig.activeSelf)
        {
            rigidbody.drag = 1;
        }
        else
        {
            rigidbody.drag = 5;
        }
    }
}
