using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    [SerializeField]
    Transform spawnLocation;
    PlayerController player;
    bool transition = false;
    private const float FIXED_SCALE = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.transform.position = (transform.position + (PlayerInfo.piInstance.normalized * PlayerInfo.piInstance.magnitude * FIXED_SCALE));
        player.Fade(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transition && !player.Fading())
        {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(PlayerInfo.piInstance.currentScene);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>();
        PlayerInfo.piInstance.magnitude = (other.transform.position - transform.position).magnitude;
        PlayerInfo.piInstance.normalized = (other.transform.position - transform.position).normalized;
        PlayerInfo.piInstance.spawnLocation = (PlayerInfo.piInstance.lastTownPosition + ((PlayerInfo.piInstance.normalized * PlayerInfo.piInstance.magnitude) / FIXED_SCALE));
        player.Fade(true);
        transition = true;        
    }
}
