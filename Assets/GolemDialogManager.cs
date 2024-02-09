using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class GolemDialogManager : CharacterDialogManager
{
    GolemTriggerManager golemTriggerManager;
    StoryManager storyManager;
    GolemEventManager golemEventManager;

    protected override void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = inputManager.GetComponent<AnimatorManager>();
        golemTriggerManager = GetComponent<GolemTriggerManager>();
        storyManager = FindObjectOfType<StoryManager>();
        golemEventManager = GetComponent<GolemEventManager>();
    }

    public override void StartDialogue()
    {
        canDialog = true;
        playerStatsUi.SetActive(false);
    }

    public void HandleDialogue (int k)
    {
        if(!canDialog) return;

        if(i < golemTriggerManager.dialogDatas[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = golemTriggerManager.dialogDatas[k].characterName;
            else actorName.text = golemTriggerManager.dialogDatas[k].mainCharacterName;

            actorSentence.text = golemTriggerManager.dialogDatas[k].firstConversation[i];
            nextFirstDialogue(k, golemTriggerManager.dialogDatas);
        }        
    }

    public override void CloseDialogue()
    {
        golemTriggerManager.EndDialogue();
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
                CloseDialogue();
                if(storyManager.storyStep == 2)
                {
                    StartCoroutine(golemEventManager.golemAchievement());
                }
            }
        }

    }
}
