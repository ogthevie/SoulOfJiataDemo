using UnityEngine;

public class GolemTriggerManager : MonoBehaviour
{
    public DialogData [] dialogDatas = new DialogData [2];
    protected GolemDialogManager golemDialogManager;
    protected StoryManager storyManager;
    protected AudioSource audioSource;
    public GameObject dialogUI;
    protected int idDialog;

    void Awake()
    {
        golemDialogManager = GetComponent<GolemDialogManager>();
        storyManager = FindFirstObjectByType<StoryManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        dialogUI = GameObject.Find("PlayerUI").transform.GetChild(7).gameObject;
        storyManager = FindFirstObjectByType<StoryManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(storyManager.storyStep == 1 || storyManager.storyStep == 6 || storyManager.storyStep == 7)
        {
            if(storyManager.storyStep == 1) idDialog = 0;
            else if(storyManager.storyStep == 6) idDialog = 1;
            else idDialog = 2;

            if(other.gameObject.layer == 3)
            {
                dialogUI.SetActive(true);
                golemDialogManager.StartDialogue();
                Time.timeScale = 0f;
                audioSource.Play();
            } 
        }

        
    }

    void OnTriggerStay(Collider other)
    {
        if(storyManager.storyStep == 1 || storyManager.storyStep == 6 || storyManager.storyStep == 7)
        {
            golemDialogManager.HandleDialogue(idDialog);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(storyManager.storyStep == 1 || storyManager.storyStep == 6 || storyManager.storyStep == 7)
        {
            golemDialogManager.CloseDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
    }  
}
