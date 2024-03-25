using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            door.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            door.SetActive(true);
        }
    }
}
