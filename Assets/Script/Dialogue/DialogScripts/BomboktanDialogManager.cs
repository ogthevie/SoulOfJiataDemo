using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class BomboktanDialogManager : CharacterDialogManager
{
    BomboktanTriggerManager bomboktanTriggerManager;
    GameSaveManager gameSaveManager;
    protected override void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        bomboktanTriggerManager = GetComponent<BomboktanTriggerManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
    }

    public override void StartDialogue()
    {
        canDialog = true;
        playerUIManager.HiddenUI();
    }

    public void HandleDialogue(int k)
    {
        if(!canDialog)
            return;


        if(i < bomboktanTriggerManager.dialogDatas[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = bomboktanTriggerManager.dialogDatas[k].characterName;
            else actorName.text = bomboktanTriggerManager.dialogDatas[k].mainCharacterName;

            actorSentence.text = bomboktanTriggerManager.dialogDatas[k].firstConversation[i];
            nextFirstDialogue(k, bomboktanTriggerManager.dialogDatas);
        }
    }

    public override void CloseDialogue()
    {
        bomboktanTriggerManager.EndDialogue();
        canDialog = false;
        
        playerUIManager.ShowUI();
     
    }

    public override void nextFirstDialogue(int k, DialogData[] characterDialogData)
    {
        if(inputManager.InteractFlag && i < characterDialogData[k].firstConversation.Count) 
        {
            i++;
            if(i >= characterDialogData[k].firstConversation.Count)
            {
                i = 0;
                CloseDialogue();
            }
        }

    }
}
