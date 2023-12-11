using UnityEngine;
using TMPro;
using SJ;
using System;

public class CharacterDialogManager : MonoBehaviour
{
    public DialogData[] characterDialogData = new DialogData [5];
    InputManager inputManager;
    AnimatorManager animatorManager;

    protected Animator characterAnimator;
    public DialogTriggerManager dialogTriggerManager;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI actorSentence;
    public GameObject playerStatsUi;
    int i = 0;
    public bool canDialog;

    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
        dialogTriggerManager = GetComponent<DialogTriggerManager>();
        characterAnimator = GetComponent<Animator>();
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
        characterAnimator.SetBool("animState", true);
    }

    public virtual void HandleDialogue(int k)
    {
        if(!canDialog)
            return;


        if(!characterDialogData[k].fConv && i < characterDialogData[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = characterDialogData[k].characterName;
            else actorName.text = characterDialogData[k].mainCharacterName;

            actorSentence.text = characterDialogData[k].firstConversation[i];
            nextFirstDialogue(k);
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
        animatorManager.animationState = true;
        characterAnimator.SetBool("animState", false);
    }

    public virtual void nextFirstDialogue(int k)
    {
        if(inputManager.InteractFlag && i < characterDialogData[k].firstConversation.Count) 
        {
            animatorManager.animationState = false;
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
