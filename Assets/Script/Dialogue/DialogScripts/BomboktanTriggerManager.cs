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
    protected int idDialog;

    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        bomboktanDialogManager = GetComponent<BomboktanDialogManager>();
        storyManager = FindObjectOfType<StoryManager>();
    }


    void Start()
    {
        dialogUI = GameObject.Find("Player UI").transform.GetChild(2).gameObject;
        storyManager = FindObjectOfType<StoryManager>();
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if(storyManager.storyStep == 5) idDialog = 0;
        else if(storyManager.storyStep == 6) idDialog = 1;
        if(other.gameObject.layer == 3)
        {
            dialogUI.SetActive(true);
            bomboktanDialogManager.StartDialogue();
            Time.timeScale = 0f;
        }         
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
