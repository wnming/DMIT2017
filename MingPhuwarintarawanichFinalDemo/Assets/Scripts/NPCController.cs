using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject[] colliders;
    private bool isTalkedToPlayer;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string[] messages;
    [SerializeField] GameObject levelQuestPanel;

    [SerializeField] RawImage npcImageInPanel;
    [SerializeField] Texture npcImageTexture;

    [SerializeField] AudioSource helloSound;
    [SerializeField] AudioSource levelUp;

    [SerializeField] GameObject conversation;

    [SerializeField] GameObject level2Collider;

    GameObject player;
    float range = 7.0f;

    [SerializeField] int npcNumber;

    void Start()
    {
        conversation.SetActive(false);
        levelQuestPanel.SetActive(false);
        isTalkedToPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            conversation.SetActive(true);
        }
        else
        {
            conversation.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isTalkedToPlayer)
        {
            helloSound.Play();
            isTalkedToPlayer = true;
            StartCoroutine(DisplayMessages());
        }
    }

    IEnumerator DisplayMessages()
    {
        npcImageInPanel.texture = npcImageTexture;
        levelQuestPanel.SetActive(true);
        Time.timeScale = 0;
        if(npcNumber == 2 && DataManager.numberOfKilledGhosts < DataManager.LEVEL_1_GHOSTS)
        {
            text.text = "You need to kill all enemies in order to proceed to level 2!";
            yield return new WaitForSecondsRealtime(3);
            text.text = "Hurry up! You're runnning out of time!!!";
            yield return new WaitForSecondsRealtime(3);
            isTalkedToPlayer = false;
        }
        else
        {
            for (int index = 0; index < messages.Length; index++)
            {
                text.text = messages[index];
                yield return new WaitForSecondsRealtime(3.5f);
            }
            for (int index = 0; index < colliders.Length; index++)
            {
                colliders[index].SetActive(false);
            }
            gameObject.SetActive(false);
            if(npcNumber == 2)
            {
                player.GetComponent<PlayerController>().ChangeWeapon();
                level2Collider.SetActive(false);
                DataManager.currentLevel = 2;
                DataManager.numberOfKilledGhosts = 0;
                levelUp.Play();
            }
        }
        levelQuestPanel.SetActive(false);
        Time.timeScale = 1;
        DataManager.isTimeRun = true;
    }
}
