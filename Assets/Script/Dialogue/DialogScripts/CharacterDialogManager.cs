using UnityEngine;
using TMPro;
using SJ;
using DG.Tweening;

public class CharacterDialogManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected InputManager inputManager;
    protected StoryManager storyManager;
    protected AnimatorManager animatorManager;

    protected Animator characterAnimator;
    public DialogTriggerManager dialogTriggerManager;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI actorSentence;
    protected PlayerUIManager playerUIManager;
    protected Camera mainCamera;
    protected int i = 0;
    
    public bool canDialog;

    protected virtual void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        storyManager = gameManager.GetComponent<StoryManager>();
        inputManager = FindFirstObjectByType<InputManager>();
        animatorManager = FindFirstObjectByType<AnimatorManager>();
        playerUIManager = FindFirstObjectByType<PlayerUIManager>();
        dialogTriggerManager = GetComponent<DialogTriggerManager>();
        characterAnimator = GetComponent<Animator>();
        mainCamera = FindFirstObjectByType<Camera>();
    }



    protected void Start()
    {
        GameObject playerUI = GameObject.Find("PlayerUI");
        actorName = playerUI.transform.GetChild(7).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        actorSentence = playerUI.transform.GetChild(7).GetChild(1).GetComponent<TextMeshProUGUI>();

    }

    public virtual void StartDialogue()
    {
        canDialog = true;
        playerUIManager.HiddenUI();
        playerUIManager.ShowInteractionUI("Parler");
        characterAnimator.SetBool("animState", true);
    }

    public virtual void HandleDialogue(int k, DialogData[] characterDialogData)
    {
        if(!canDialog)
            return;


        if(i < characterDialogData[k].firstConversation.Count)
        {
            /*if(i % 2 == 0)*/ actorName.text = characterDialogData[k].characterName;
            //else actorName.text = characterDialogData[k].mainCharacterName;

            actorSentence.text = characterDialogData[k].firstConversation[i];
            nextFirstDialogue(k, characterDialogData);
        }
                //dialogTriggerManager.storyManager.checkstoryStep(characterDialogData[k].canNextStep); 
    }

    public virtual void CloseDialogue()
    {
        dialogTriggerManager.EndDialogue();
        canDialog = false;
        animatorManager.animationState = true;
        characterAnimator.SetBool("animState", false);
        playerUIManager.ShowUI();
        playerUIManager.HiddenInteractionUI();
        mainCamera.DOFieldOfView(35, 0.5f);
    }

    public virtual void nextFirstDialogue(int k, DialogData[] characterDialogData)
    {
        if(inputManager.InteractFlag && i < characterDialogData[k].firstConversation.Count) 
        {
            inputManager.InteractFlag = false;
            mainCamera.DOFieldOfView(18, 0.5f);
            //Debug.Log("i est egal à : " +i);
            animatorManager.animationState = false;
            i++;
            if(i >= characterDialogData[k].firstConversation.Count)
            {
                //Debug.Log("le dialogue est long de" +characterDialogData[k].firstConversation.Count);
                i = 0;
                CloseDialogue();
                FindFirstObjectByType<GameManager>().GlobalFixedCursorPosition();
            }
        }
    }
}
