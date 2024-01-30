using System.Collections;
using System.Collections.Generic;
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
            PlayerInfo.piInstance.spawnLocation = exitLocation.position;
            PlayerInfo.piInstance.currentScene = townName;
            player = other.GetComponent<PlayerController>();
            player.Fade(true);
            transition = true;
        }
    }
}
