using UnityEngine;
using TMPro;
using SJ;

public class CharacterDialogManager : MonoBehaviour
{
    public DialogData characterDialogData;
    InputManager inputManager;
    PlayerManager playerManager;
    public DialogTriggerManager dialogTriggerManager;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI actorSentence;
    int i = 0;
    public bool canDialog;

    void Awake()
    {
        inputManager = FindAnyObjectByType<InputManager>();
        dialogTriggerManager = GetComponent<DialogTriggerManager>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    void Start()
    {
        characterDialogData.fConv = false;
        characterDialogData.sConv = false;
    }

    public void StartDialogue()
    {
        canDialog = true;
    }

    public void HandleDialogue()
    {
        if(!canDialog)
            return;

        if(!characterDialogData.fConv)
        {

            if(i < characterDialogData.firstConversation.Count)
            {
                if(i % 2 == 0) actorName.text = characterDialogData.characterName;
                else actorName.text = characterDialogData.mainCharacterName;

                actorSentence.text = characterDialogData.firstConversation[i];
                nextFirstDialogue();
            }
        }

        else if(characterDialogData.fConv && !characterDialogData.sConv)
        {

            if(i < characterDialogData.secondConversation.Count)
            {
                if(i % 2 == 0) actorName.text = characterDialogData.characterName;
                else actorName.text = characterDialogData.mainCharacterName;

                actorSentence.text = characterDialogData.secondConversation[i];
                nextSecondDialogue();
            }        
        }

        else if(characterDialogData.sConv)
        {
            actorName.text = characterDialogData.characterName;
            actorSentence.text = characterDialogData.thirdConversation[i];
        }
    }

    public void CloseDialogue()
    {
        dialogTriggerManager.EndDialogue();
        canDialog = false;
    }

    void nextFirstDialogue()
    {
        if(inputManager.InteractFlag && i < characterDialogData.firstConversation.Count) 
        {
            i++;
            if(i >= characterDialogData.firstConversation.Count)
            {
                i = 0;
                characterDialogData.fConv = true;
                CloseDialogue();
            }
        }

    }   

    void nextSecondDialogue()
    {
        if(inputManager.InteractFlag && i < characterDialogData.secondConversation.Count)
        {
            i++;
            if(i >= characterDialogData.secondConversation.Count)
            {
                i = 0;
                characterDialogData.sConv = true;
                CloseDialogue();
            }
        }

    } 

}
