using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class BomboktanDialogManager : CharacterDialogManager
{
    BomboktanTriggerManager bomboktanTriggerManager;
    protected override void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
        bomboktanTriggerManager = GetComponent<BomboktanTriggerManager>();
    }

    public override void StartDialogue()
    {
        canDialog = true;
        playerStatsUi.SetActive(false);
    }

    public void HandleDialogue(int k)
    {
        if(!canDialog)
            return;


        if(!bomboktanTriggerManager.dialogDatas[k].fConv && i < bomboktanTriggerManager.dialogDatas[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = bomboktanTriggerManager.dialogDatas[k].characterName;
            else actorName.text = bomboktanTriggerManager.dialogDatas[k].mainCharacterName;

            actorSentence.text = bomboktanTriggerManager.dialogDatas[k].firstConversation[i];
            nextFirstDialogue(k, bomboktanTriggerManager.dialogDatas);
        }
    

        else if(bomboktanTriggerManager.dialogDatas[k].fConv)
        {

                actorName.text = bomboktanTriggerManager.dialogDatas[k].characterName;
                actorSentence.text = bomboktanTriggerManager.dialogDatas[k].secondConversation[i];   
        }
    }

    public override void CloseDialogue()
    {
        bomboktanTriggerManager.EndDialogue();
        playerStatsUi.SetActive(true);
        canDialog = false;        
    }

    public override void nextFirstDialogue(int k, DialogData[] characterDialogData)
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
