using UnityEngine;
using SJ;


public class DialogTriggerManager : MonoBehaviour
{
    public DialogData [] partZero = new DialogData[5];
    public DialogData [] partOne = new DialogData[5];
    public DialogData [] partTwo = new DialogData[5];
    public DialogData [] partThree = new DialogData[5];
    public DialogData [] partFour = new DialogData[5];
    public DialogData [] partsix = new DialogData[5];
    public DialogData [] partSeven = new DialogData[5];
    
    public DialogData[][] partsManager = new DialogData[8][];
    protected CharacterDialogManager characterDialogManager;
    protected AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        InitializePartsManager();
    }

    void Start()
    {
        dialogUI = GameObject.Find("PlayerUI").transform.GetChild(7).gameObject;
        storyManager = FindObjectOfType<StoryManager>();
    }

    void InitializePartsManager()
    {
        partsManager[0] = partZero;
        partsManager[1] = partOne;
        partsManager[2] = partTwo;
        partsManager[3] = partThree;
        partsManager[4] = partFour;
        partsManager[5] = null;
        partsManager[6] = partsix;
        partsManager[7] = partSeven;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if(FindObjectOfType<StoryManager>().storyStep == -1) return;
        
        if(other.gameObject.layer == 3)
        {
            idDialog = sibongoManager.dayPeriod;
            dialogUI.SetActive(true);
            characterDialogManager.StartDialogue();
            audioSource.Play();
            //Time.timeScale = 0f;
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
        //Time.timeScale = 1f;
    }
}
