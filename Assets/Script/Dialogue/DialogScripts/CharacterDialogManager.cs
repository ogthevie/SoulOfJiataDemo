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
    public GameObject playerStatsUi;
    protected int i = 0;
    public bool canDialog;

    protected virtual void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
        dialogTriggerManager = GetComponent<DialogTriggerManager>();
        characterAnimator = GetComponent<Animator>();
    }



    protected void Start()
    {
        actorName = GameObject.Find("Player UI").transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        actorSentence = GameObject.Find("Player UI").transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerStatsUi = GameObject.Find("Player UI").transform.GetChild(0).gameObject;
    }



    public virtual void StartDialogue()
    {
        canDialog = true;
        playerStatsUi.SetActive(false);
        characterAnimator.SetBool("animState", true);
    }

    public virtual void HandleDialogue(int k, DialogData[] characterDialogData)
    {
        if(!canDialog)
            return;


        if(!characterDialogData[k].fConv && i < characterDialogData[k].firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = characterDialogData[k].characterName;
            else actorName.text = characterDialogData[k].mainCharacterName;

            actorSentence.text = characterDialogData[k].firstConversation[i];
            nextFirstDialogue(k, characterDialogData);
        }
    

        else if(characterDialogData[k].fConv)
        {

                actorName.text = characterDialogData[k].characterName;
                actorSentence.text = characterDialogData[k].secondConversation[i];
                dialogTriggerManager.storyManager.checkstoryStep(characterDialogData[k].canNextStep); 
        }
    }

    public virtual void CloseDialogue()
    {
        dialogTriggerManager.EndDialogue();
        playerStatsUi.SetActive(true);
        canDialog = false;
        animatorManager.animationState = true;
        characterAnimator.SetBool("animState", false);
    }

    public virtual void nextFirstDialogue(int k, DialogData[] characterDialogData)
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
