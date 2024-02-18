using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    PlayerManager player;
    bool transition = false;
    private const float FIXED_SCALE = 3f;

    [SerializeField] GameObject treasure;
    [SerializeField] string townName;

    private bool isLoadFromFile = false;

    void Awake()
    {
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            isLoadFromFile = true;
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        player.transform.position = isLoadFromFile ? SaveInfo.playerInfo.spawnLocation : (transform.position + (SaveInfo.playerInfo.normalized * SaveInfo.playerInfo.magnitude * FIXED_SCALE));
        if (treasure != null)
        {
            treasure.SetActive(false);
        }
    }

    void Update()
    {
        if (transition)
        {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(SaveInfo.playerInfo.currentScene);
        }

        if(treasure != null)
        {
            if (SaveInfo.playerInfo.townInfoList.Where(x => x.TownName == townName).FirstOrDefault().enemyHealth.Where(y => y <= 0).Count() == 3 && !SaveInfo.playerInfo.townInfoList.Where(x => x.TownName == townName).FirstOrDefault().isTreasureCollected)
            {
                treasure.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SaveInfo.playerInfo.magnitude = (other.transform.position - transform.position).magnitude;
            SaveInfo.playerInfo.normalized = (other.transform.position - transform.position).normalized;
            SaveInfo.playerInfo.spawnLocation = (SaveInfo.playerInfo.lastTownPosition + ((SaveInfo.playerInfo.normalized * SaveInfo.playerInfo.magnitude) / FIXED_SCALE));
            transition = true;
        }
    }
}
