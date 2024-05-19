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
    PlayerStats playerStats;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        playerStats = playerManager.GetComponent<PlayerStats>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
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
        else if(inputManager.south_input) 
        {
            StartCoroutine (reloadRoutine());
        }
    }

    IEnumerator reloadRoutine()
    {
        inputManager.skillTreeManager.HandleSkillTreeUI(false);
        inputManager.GetComponent<AudioManager>().jiataAudioSource.Stop();
        
        string filePath = Application.persistentDataPath + "/playerData.json";

        if(System.IO.File.Exists(filePath))
        {
            gameManager.newGame = 0;
            gameSaveManager.LoadAllData();
            playerStats.AddStamina(100);
            playerManager.isDead = playerManager.isInteracting = false;
            float activeScene = SceneManager.GetActiveScene().buildIndex;
            if(activeScene == 2)
            {
                gameSaveManager.LoadGrotteData();
                gameSaveManager.LoadTorcheGrotteData();
            }
            yield return new WaitForSeconds (0.5f);
            this.gameObject.SetActive(false);
        }
    }
}
