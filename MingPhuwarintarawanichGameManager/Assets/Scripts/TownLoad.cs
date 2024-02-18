using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownLoad : MonoBehaviour
{
    [SerializeField] string townName;
    bool transition = false;

    private void Update()
    {
        if (transition)
        {
            SceneManager.LoadScene(townName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Overworld");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveInfo.playerInfo.currentScene = townName;
            SaveInfo.playerInfo.magnitude = (other.transform.position - transform.position).magnitude;
            SaveInfo.playerInfo.normalized = (other.transform.position - transform.position).normalized;
            SaveInfo.playerInfo.lastTownPosition = transform.position;
            transition = true;
        }
    }
}
