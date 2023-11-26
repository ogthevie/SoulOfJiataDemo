using UnityEngine;
using TMPro;
using SJ;
using System;

public class CharacterDialogManager : MonoBehaviour
{
    public DialogData[] characterDialogData = new DialogData [5];
    InputManager inputManager;
    PlayerManager playerManager;
    public DialogTriggerManager dialogTriggerManager;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI actorSentence;
    GameObject playerStatsUi;
    int i = 0;
    int k = 0;
    public bool canDialog;

    void Awake()
    {
        inputManager = FindAnyObjectByType<InputManager>();
        dialogTriggerManager = GetComponent<DialogTriggerManager>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    void Start()
    {
        actorName = GameObject.Find("Player UI").transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        actorSentence = GameObject.Find("Player UI").transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerStatsUi = GameObject.Find("Player UI").transform.GetChild(0).gameObject;
    }

    public void StartDialogue()
    {
        canDialog = true;
        playerStatsUi.SetActive(false);
    }

    public void HandleDialogue()
    {
        if(!canDialog)
            return;


        if(!characterDialogData[k].fConv && i < characterDialogData[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = characterDialogData[k].characterName;
            else actorName.text = characterDialogData[k].mainCharacterName;

            actorSentence.text = characterDialogData[k].firstConversation[i];
            nextFirstDialogue();
        }
    

        else if(characterDialogData[k].fConv)
        {

                actorName.text = characterDialogData[k].characterName;
                actorSentence.text = characterDialogData[k].secondConversation[i];   
        }
    }

    public void CloseDialogue()
    {
        dialogTriggerManager.EndDialogue();
        playerStatsUi.SetActive(true);
        canDialog = false;
    }

    void nextFirstDialogue()
    {
        if(inputManager.InteractFlag && i < characterDialogData[k].firstConversation.Count) 
        {
            i++;
            if(i >= characterDialogData[k].firstConversation.Count)
            {
                i = 0;
                characterDialogData[k].fConv = true;
                CloseDialogue();
            }
        }

    }

}
