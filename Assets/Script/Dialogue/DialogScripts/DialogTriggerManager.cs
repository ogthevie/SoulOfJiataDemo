using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;


public class DialogTriggerManager : MonoBehaviour
{
    CharacterDialogManager characterDialogManager;
    public CameraManager cameraManager;
    public GameObject dialogUI;

    void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        characterDialogManager = GetComponent<CharacterDialogManager>();
    }

    void OnTriggerEnter(Collider other)
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
        characterDialogManager.HandleDialogue();
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
