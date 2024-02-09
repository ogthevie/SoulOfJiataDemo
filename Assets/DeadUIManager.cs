using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class DeadUIManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    GameSaveManager gameSaveManager;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
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
        gameSaveManager.LoadAllData();
        animatorManager.PlayTargetAnimation("Falling", true);
        yield return new WaitForSeconds (2f);
        this.gameObject.SetActive(false);

    }
}
