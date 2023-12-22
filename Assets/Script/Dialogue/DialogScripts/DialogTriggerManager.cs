using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;
using System;


public class DialogTriggerManager : MonoBehaviour
{
    public DialogData [] partZero = new DialogData[5];
    public DialogData [] partOne = new DialogData[5];
    public DialogData [] partTwo = new DialogData[5];
    public DialogData [] partThree = new DialogData[5];
    public DialogData [] partFour = new DialogData[5];
    
    public DialogData[][] partsManager = new DialogData[5][];
    protected CharacterDialogManager characterDialogManager;
    public  StoryManager storyManager;
    CameraManager cameraManager;
    SibongoManager sibongoManager;
    public GameObject dialogUI;
    public int idDialog;

    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        characterDialogManager = GetComponent<CharacterDialogManager>();
        storyManager = FindObjectOfType<StoryManager>();
        sibongoManager = FindObjectOfType<SibongoManager>();
    }
    void OnEnable()
    {
        InitializePartsManager();
    }

    void Start()
    {
        dialogUI = GameObject.Find("Player UI").transform.GetChild(2).gameObject;
        storyManager = FindObjectOfType<StoryManager>();
    }

    void InitializePartsManager()
    {
        partsManager[0] = partZero;
        partsManager[1] = partOne;
        partsManager[2] = partTwo;
        partsManager[3] = partThree;
        partsManager[4] = partFour;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            idDialog = sibongoManager.dayPeriod;
            dialogUI.SetActive(true);
            characterDialogManager.StartDialogue();
            Time.timeScale = 0f;
        }         
    }

    void OnTriggerStay(Collider other)
    {
        characterDialogManager.HandleDialogue(idDialog, partsManager[storyManager.storyStep]);

    }

    void OnTriggerExit(Collider other)
    {
        characterDialogManager.CloseDialogue();
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
