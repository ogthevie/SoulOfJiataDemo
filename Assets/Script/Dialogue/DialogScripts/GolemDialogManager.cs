using SJ;

public class GolemDialogManager : CharacterDialogManager
{
    GolemTriggerManager golemTriggerManager;
    GolemEventManager golemEventManager;

    protected override void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        storyManager = gameManager.GetComponent<StoryManager>();
        inputManager = FindFirstObjectByType<InputManager>();
        animatorManager = inputManager.GetComponent<AnimatorManager>();
        playerUIManager = FindFirstObjectByType<PlayerUIManager>();
        golemTriggerManager = GetComponent<GolemTriggerManager>();
        golemEventManager = GetComponent<GolemEventManager>();
    }

    public override void StartDialogue()
    {
        canDialog = true;
        playerUIManager.HiddenUI();
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
        playerUIManager.ShowUI();
        canDialog = false;        
    }

    public override void nextFirstDialogue(int k, DialogData[] characterDialogData)
    {
        if(inputManager.InteractFlag && i < characterDialogData[k].firstConversation.Count) 
        {
            inputManager.InteractFlag = false;
            i++;
            if(i >= characterDialogData[k].firstConversation.Count)
            {
                i = 0;
                CloseDialogue();
                if(storyManager.storyStep == 6)
                {
                    StartCoroutine(golemEventManager.BridgeEvent());
                }
            }
        }
    }
}
