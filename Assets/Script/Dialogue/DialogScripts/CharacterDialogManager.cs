using UnityEngine;
using TMPro;
using SJ;

public class CharacterDialogManager : MonoBehaviour
{

    protected InputManager inputManager;
    protected AnimatorManager animatorManager;

    protected Animator characterAnimator;
    public DialogTriggerManager dialogTriggerManager;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI actorSentence;
    protected PlayerUIManager playerUIManager;
    protected int i = 0;
    
    public bool canDialog;

    protected virtual void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        dialogTriggerManager = GetComponent<DialogTriggerManager>();
        characterAnimator = GetComponent<Animator>();
    }



    protected void Start()
    {
        GameObject playerUI = GameObject.Find("Player UI");
        actorName = playerUI.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        actorSentence = playerUI.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    }

    public virtual void StartDialogue()
    {
        canDialog = true;
        playerUIManager.HiddenUI();
        characterAnimator.SetBool("animState", true);
    }

    public virtual void HandleDialogue(int k, DialogData[] characterDialogData)
    {
        if(!canDialog)
            return;


        if(i < characterDialogData[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = characterDialogData[k].characterName;
            else actorName.text = characterDialogData[k].mainCharacterName;

            actorSentence.text = characterDialogData[k].firstConversation[i];
            nextFirstDialogue(k, characterDialogData);
        }
                //dialogTriggerManager.storyManager.checkstoryStep(characterDialogData[k].canNextStep); 
    }

    public virtual void CloseDialogue()
    {
        dialogTriggerManager.EndDialogue();
        playerUIManager.ShowUI();
        canDialog = false;
        animatorManager.animationState = true;
        characterAnimator.SetBool("animState", false);
    }

    public virtual void nextFirstDialogue(int k, DialogData[] characterDialogData)
    {
        if(inputManager.InteractFlag && i < characterDialogData[k].firstConversation.Count) 
        {
            inputManager.InteractFlag = false;
            //Debug.Log("i est egal à : " +i);
            animatorManager.animationState = false;
            i++;
            if(i >= characterDialogData[k].firstConversation.Count)
            {
                Debug.Log("le dialogue est long de" +characterDialogData[k].firstConversation.Count);
                i = 0;
                CloseDialogue();
                FindObjectOfType<GameManager>().GlobalFixedCursorPosition();
            }
        }
    }
}
