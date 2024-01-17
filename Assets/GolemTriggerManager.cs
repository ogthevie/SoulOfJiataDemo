using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class GolemTriggerManager : MonoBehaviour
{
    public DialogData [] dialogDatas = new DialogData [2];
    protected GolemDialogManager golemDialogManager;
    protected StoryManager storyManager;
    CameraManager cameraManager;
    public GameObject dialogUI;
    protected int idDialog;

    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        golemDialogManager = GetComponent<GolemDialogManager>();
        storyManager = FindObjectOfType<StoryManager>();
    }

    void Start()
    {
        dialogUI = GameObject.Find("Player UI").transform.GetChild(2).gameObject;
        storyManager = FindObjectOfType<StoryManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(storyManager.storyStep == 2) idDialog = 0;
        else if(storyManager.storyStep == 3) idDialog = 1;

        if(other.gameObject.layer == 3)
        {
            dialogUI.SetActive(true);
            golemDialogManager.StartDialogue();
            Time.timeScale = 0f;
        }         
    }

    void OnTriggerStay(Collider other)
    {
        golemDialogManager.HandleDialogue(idDialog);
    }

    void OnTriggerExit(Collider other)
    {
        golemDialogManager.CloseDialogue();
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
    }  
}
