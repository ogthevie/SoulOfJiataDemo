using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;


public class DialogTriggerManager : MonoBehaviour
{
    public Vector3 [] positions = new Vector3[5];
    protected CharacterDialogManager characterDialogManager;
    public StoryManager storyManager;
    public CameraManager cameraManager;
    public GameObject dialogUI;
    public int idDialog;

    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        characterDialogManager = GetComponent<CharacterDialogManager>();
    }

    void Start()
    {
        dialogUI = GameObject.Find("Player UI").transform.GetChild(2).gameObject;
        storyManager = FindObjectOfType<StoryManager>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            //Debug.Log(this.gameObject.transform.position);
            dialogUI.SetActive(true);
            characterDialogManager.StartDialogue();
        }         
    }

    void OnTriggerStay(Collider other)
    {
        characterDialogManager.HandleDialogue(idDialog);
    }

    void OnTriggerExit(Collider other)
    {
        characterDialogManager.CloseDialogue();
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
    }
}
