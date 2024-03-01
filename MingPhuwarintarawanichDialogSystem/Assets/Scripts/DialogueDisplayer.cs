using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    public DialogueObject playerDialogue;
    public DialogueObject copDialogue;
    public DialogueObject robberDialogue;

    public GameObject player;
    public GameObject piggy;
    public GameObject cop;
    public GameObject robber;

    private bool isFirstmeetTheCop;
    private bool isFirstmeetTheRobber;
    private bool isHavingConversation;
    private bool isDoneDiscussing;

    private void Start()
    {
        isFirstmeetTheCop = true;
        isFirstmeetTheRobber = true;
        isHavingConversation = false;
        dialogueBox.SetActive(false);
        piggy.SetActive(false);
        isDoneDiscussing = true;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, cop.transform.position) < 3 && !isHavingConversation)
        {
            isHavingConversation = true;
            dialogueBox.SetActive(true);
            DisplayDialogue(copDialogue, "cop");
        }
        else
        {
            if (Vector3.Distance(player.transform.position, cop.transform.position) >= 3 && Vector3.Distance(player.transform.position, robber.transform.position) > 3)
            {
                isHavingConversation = false;
                piggy.SetActive(false);
                dialogueBox.SetActive(false);
            }
        }

        if (Vector3.Distance(player.transform.position, robber.transform.position) < 3 && !isHavingConversation)
        {
            isHavingConversation = true;
            dialogueBox.SetActive(true);
            DisplayDialogue(robberDialogue, "robber");
        }
        else
        {
            if (Vector3.Distance(player.transform.position, robber.transform.position) >= 3 && Vector3.Distance(player.transform.position, cop.transform.position) > 3)
            {
                isHavingConversation = false;
                dialogueBox.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator MoveThroughDialogue(DialogueObject dialogueObject, string npcName)
    {
        int tempPlayerIndex = 0;
        int tempNpcIndex = 0;
        for (int i = 0; i < dialogueObject.dialogueLines.Length - 1; i++)
        {
            tempNpcIndex = i;
            if(i == 0)
            {
                tempPlayerIndex = npcName == "robber" ? i + 2 : i;
            }
            else
            {
                if(npcName == "cop")
                {
                    tempPlayerIndex = i;
                }
            }

            //cop
            if (npcName == "cop")
            {
                if (isFirstmeetTheCop)
                {
                    isFirstmeetTheCop = false;
                }
                else
                {
                    tempNpcIndex += 1;
                }
                if(tempNpcIndex == 2)
                {
                    piggy.SetActive(true);
                }
            }
            //robber
            if (npcName == "robber")
            {
                if (isFirstmeetTheRobber)
                {
                    isFirstmeetTheRobber = false;
                }
                else
                {
                    tempNpcIndex += 1;
                }
            }
            dialogueText.text = dialogueObject.dialogueLines[tempNpcIndex].dialogue;

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitForSeconds(0.1f);
            dialogueText.text = playerDialogue.dialogueLines[tempPlayerIndex].dialogue;
            if (npcName == "robber")
            {
                tempPlayerIndex += 1;
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitForSeconds(0.1f);
        }
        dialogueBox.SetActive(false);
    }

    public void DisplayDialogue(DialogueObject dialogueObject, string npcName)
    {
        StartCoroutine(MoveThroughDialogue(dialogueObject, npcName));
    }
}