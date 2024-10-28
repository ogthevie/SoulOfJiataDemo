using UnityEngine;

public class MbuuTriggerManager : MonoBehaviour
{
    public DialogData dialogData;
    protected MbuuDialogManager mbuuDialogManager;
    public GameObject dialogUI;
    [SerializeField] AudioSource audioSource;
    Animator mbuuAnim;
    protected int idDialog;

    void Awake()
    {
        mbuuDialogManager = GetComponent<MbuuDialogManager>();
        mbuuAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start() 
    {
        dialogUI = GameObject.Find("PlayerUI").transform.GetChild(7).gameObject;     
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            dialogUI.SetActive(true);
            mbuuAnim.SetBool("isTalk", true);
            mbuuDialogManager.StartDialogue();
            audioSource.Play();
            Time.timeScale = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {

        mbuuDialogManager.HandleDialogue(idDialog);    
    }

    void OnTriggerExit(Collider other)
    {
        mbuuDialogManager.CloseDialogue();
        mbuuAnim.SetBool("isTalk", false); 
    }

    public void EndDialogue()
    {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
    }  
}
