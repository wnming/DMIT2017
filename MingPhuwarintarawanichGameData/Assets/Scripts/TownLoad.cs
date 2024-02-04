using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownLoad : MonoBehaviour
{
    [SerializeField]
    Transform exitLocation;
    [SerializeField]
    string townName;
    bool transition = false;
    PlayerController player;

    private void Update()
    {
        if (transition && !player.Fading())
        {
            SceneManager.LoadScene(townName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Overworld");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInfo.piInstance.currentScene = townName;
            player = other.GetComponent<PlayerController>();
            PlayerInfo.piInstance.magnitude = (other.transform.position - transform.position).magnitude;
            PlayerInfo.piInstance.normalized = (other.transform.position - transform.position).normalized;
            PlayerInfo.piInstance.lastTownPosition = transform.position;
            player.Fade(true);
            transition = true;
        }
    }
}
