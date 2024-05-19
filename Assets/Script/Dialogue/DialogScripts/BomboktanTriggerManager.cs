using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class BomboktanTriggerManager : MonoBehaviour
{
    public DialogData [] dialogDatas = new DialogData [2];
    protected BomboktanDialogManager bomboktanDialogManager;
    protected StoryManager storyManager;
    CameraManager cameraManager;
    public GameObject dialogUI;
    public int idDialog;

    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        bomboktanDialogManager = GetComponent<BomboktanDialogManager>();
        storyManager = FindObjectOfType<StoryManager>();
        HandleBomboktanPosition();
    }


    void Start()
    {
        dialogUI = GameObject.Find("PlayerUI").transform.GetChild(7).gameObject;
        storyManager = FindObjectOfType<StoryManager>();
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        HandleBomboktanPosition();

        if(other.gameObject.layer == 3)
        {
            dialogUI.SetActive(true);
            bomboktanDialogManager.StartDialogue();
            //Time.timeScale = 0f;
        }         
    }

    public void HandleBomboktanPosition()
    {
        if(storyManager.storyStep == 3) idDialog = 0;
        else if(storyManager.storyStep == 31) idDialog = 1;
        else if(storyManager.storyStep == 32) idDialog = 2;
        else if(storyManager.storyStep == 4) idDialog = 3;
    }

    void OnTriggerStay(Collider other)
    {
        bomboktanDialogManager.HandleDialogue(idDialog);
    }

    void OnTriggerExit(Collider other)
    {
        bomboktanDialogManager.CloseDialogue();
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
    }    
}
