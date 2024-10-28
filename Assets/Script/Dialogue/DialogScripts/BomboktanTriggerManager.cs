using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class BomboktanTriggerManager : MonoBehaviour
{
    public DialogData [] dialogDatas = new DialogData [2];
    protected BomboktanDialogManager bomboktanDialogManager;
    protected StoryManager storyManager;
    protected AudioSource audioSource;
    public GameObject dialogUI;
    public int idDialog;

    void Awake()
    {
        bomboktanDialogManager = GetComponent<BomboktanDialogManager>();
        storyManager = FindFirstObjectByType<StoryManager>();
        audioSource = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }


    void Start()
    {
        dialogUI = GameObject.Find("PlayerUI").transform.GetChild(7).gameObject;
        storyManager = FindFirstObjectByType<StoryManager>();
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            dialogUI.SetActive(true);
            bomboktanDialogManager.StartDialogue();
            audioSource.Play();
            //Time.timeScale = 0f;
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
