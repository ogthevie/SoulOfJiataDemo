using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using SJ;

public class DeadUIManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    GameSaveManager gameSaveManager;
    GameManager gameManager;

    void Awake()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        gameSaveManager = FindFirstObjectByType<GameSaveManager>();
        gameManager = gameSaveManager.GetComponent<GameManager>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();
    }

    void Update()
    {
        ApplyChoice();
    }

    void ApplyChoice()
    {
        if(inputManager.lowAttack_input) Application.Quit();
        else if(inputManager.south_input && !gameManager.loadingScreen.enabled) StartCoroutine (reloadRoutine());
    }

    IEnumerator reloadRoutine()
    {
        GetComponentInParent<PlayerUIManager>().HiddenInteractionUI();
        this.transform.parent.GetChild(1).gameObject.SetActive(false);
        inputManager.skillTreeManager.HandleSkillTreeUI(false);
        inputManager.GetComponent<AudioManager>().jiataAudioSource.Stop();
        CharacterManager [] characterManagers = FindObjectsByType<CharacterManager>(FindObjectsSortMode.None);
        foreach (var elt in characterManagers)
        {
            elt.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds (1f);

        foreach (var elt in characterManagers)
        {
            elt.gameObject.SetActive(true);
        } 

        string filePath = Application.persistentDataPath + "/playerData.json";

        if(System.IO.File.Exists(filePath))
        {
            gameManager.newGame = 0;
            gameSaveManager.LoadAllData();
            FindFirstObjectByType<InventoryManager>().HandleItemsQty();
            playerManager.isDead = false;
            animatorManager.PlayTargetAnimation("Wake", true);
            float activeScene = SceneManager.GetActiveScene().buildIndex;
            if(activeScene == 2) gameSaveManager.LoadSteleGrotteData();
            yield return new WaitForSeconds (0.5f);
            this.gameObject.SetActive(false);
        }
        //FindFirstObjectByType<DialogTriggerManager>().EndDialogue();
    }
}
