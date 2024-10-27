using SJ;
using UnityEngine;
using System.Collections;

public class MbuuDialogManager : CharacterDialogManager
{
    MbuuTriggerManager mbuuTriggerManager;
    public AudioSource sceneAudiosource;
    public string sceneName;
    [SerializeField] ParticleSystem teleportationFx;

    protected override void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        inputManager = FindFirstObjectByType<InputManager>();
        animatorManager = inputManager.GetComponent<AnimatorManager>();
        playerUIManager = FindFirstObjectByType<PlayerUIManager>();
        mbuuTriggerManager = GetComponent<MbuuTriggerManager>();
        sceneAudiosource = GameObject.FindWithTag("Respawn").GetComponent<AudioSource>();

    }

    public override void StartDialogue()
    {
        canDialog = true;
        playerUIManager.HiddenUI();
        playerUIManager.ShowInteractionUI("Parler");
    }

    public void HandleDialogue (int k)
    {
        if(!canDialog) return;

        if(i < mbuuTriggerManager.dialogData.firstConversation.Count)
        {
            if(i % 2 == 0) actorName.text = mbuuTriggerManager.dialogData.characterName;
            else actorName.text = mbuuTriggerManager.dialogData.mainCharacterName;

            actorSentence.text = mbuuTriggerManager.dialogData.firstConversation[i];
            nextFirstDialogue(k, mbuuTriggerManager.dialogData);
        }        
    }

    public override void CloseDialogue()
    {
        mbuuTriggerManager.EndDialogue();
        playerUIManager.ShowUI();
        playerUIManager.HiddenInteractionUI();
        canDialog = false;   
    }

    public void nextFirstDialogue(int k, DialogData characterDialogData)
    {
        if(inputManager.InteractFlag && i < characterDialogData.firstConversation.Count) 
        {
            inputManager.InteractFlag = false;
            i++;
            if(i >= characterDialogData.firstConversation.Count)
            {
                i = 0;
                StartCoroutine(StartLoadingScene());
                CloseDialogue();
            }
        }
    }

    public IEnumerator StartLoadingScene()
    {   
        AnimatorManager animatorManager = FindFirstObjectByType<AnimatorManager>();
        animatorManager.PlayTargetAnimation("Waiting", false);
        yield return new WaitForSeconds(2.5f);
        teleportationFx.Play();
        yield return new WaitForSeconds (4f);
        gameManager.newGame = null;
        gameManager.loadSlider.fillAmount = 0;
        sceneAudiosource.Stop();
        gameManager.loadingScreen.enabled = true;
        yield return new WaitForSeconds(0.15f);
        gameManager.LoadScene(sceneName);
    }    
}
