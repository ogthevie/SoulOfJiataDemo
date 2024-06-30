using SJ;
using UnityEngine;

public class MbuuDialogManager : CharacterDialogManager
{
    MbuuTriggerManager mbuuTriggerManager;

    protected override void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = inputManager.GetComponent<AnimatorManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        mbuuTriggerManager = GetComponent<MbuuTriggerManager>();

    }

    public override void StartDialogue()
    {
        canDialog = true;
        playerUIManager.HiddenUI();
    }

    public void HandleDialogue (int k)
    {
        if(!canDialog) return;

        if(i < mbuuTriggerManager.dialogData.firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = mbuuTriggerManager.dialogData.characterName;
            else actorName.text = mbuuTriggerManager.dialogData.mainCharacterName;

            actorSentence.text = mbuuTriggerManager.dialogData.firstConversation[i];
            nextFirstDialogue(k, mbuuTriggerManager.dialogData);
        }        
    }

    public override void CloseDialogue()
    {
        mbuuTriggerManager.EndDialogue();
        playerUIManager.ShowUI();
        canDialog = false;   
    }

    public void nextFirstDialogue(int k, DialogData characterDialogData)
    {
        if(inputManager.InteractFlag && i < characterDialogData.firstConversation.Count) 
        {
            inputManager.InteractFlag = false;
            i++;
            if(i >= characterDialogData.firstConversation.Count)
            {
                i = 0;
                CloseDialogue();
            }
        }
    }    
}
