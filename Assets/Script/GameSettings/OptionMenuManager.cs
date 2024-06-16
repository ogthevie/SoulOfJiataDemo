#if UNITY_EDITOR
using System.Threading;
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SJ;

public class OptionMenuManager : MonoBehaviour
{
    [SerializeField] AudioSource selectorAudioSource;
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField]GameSaveManager gameSaveManager;
    GameManager gameManager;
    RectTransform selector;
    [SerializeField] AudioClip selectorSfx;
    GameObject buttonMap, baseMenu;
    float continueY, checkPointY, commandesY, quitY;

    void Awake()
    {
        baseMenu = transform.GetChild(0).gameObject;
        buttonMap = transform.GetChild(2).gameObject;
        buttonMap.SetActive(false);
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        gameManager = gameSaveManager.GetComponent<GameManager>();
        inputManager = FindObjectOfType<InputManager>();
        playerManager = inputManager.GetComponent<PlayerManager>();

        transform.GetChild(2).gameObject.SetActive(false);
        Transform choice = transform.GetChild(0);
        
        continueY = choice.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;
        checkPointY = choice.GetChild(1).GetComponent<RectTransform>().anchoredPosition.y;
        commandesY = choice.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y;
        quitY = choice.GetChild(3).GetComponent<RectTransform>().anchoredPosition.y;
        selector = choice.GetChild(4).GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        gameManager.GetComponent<StoryManager>().UpdateSynopsisPauseMenu();    
    }

    void OnDisable()
    {
        buttonMap.SetActive(false);
        baseMenu.SetActive(true);
    }

    private void Update() 
    {
        MoveSelector();
        ApplyChoice(selector.anchoredPosition.y);    
    }

    void MoveSelector()
    {
        if(baseMenu.activeSelf)
        {
            if(inputManager.up_input)
            {
                if(selector.anchoredPosition.y == continueY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, quitY);
                else if(selector.anchoredPosition.y == checkPointY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, continueY);
                else if(selector.anchoredPosition.y == commandesY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, checkPointY);
                else if(selector.anchoredPosition.y == quitY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, commandesY);
                selectorAudioSource.PlayOneShot(selectorSfx);
            }
            else if(inputManager.down_input)
            {
                if(selector.anchoredPosition.y == continueY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, checkPointY);
                else if(selector.anchoredPosition.y == checkPointY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, commandesY);
                else if(selector.anchoredPosition.y == commandesY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, quitY);
                else if(selector.anchoredPosition.y == quitY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, continueY);            
                selectorAudioSource.PlayOneShot(selectorSfx);
            }
        }
 
    }

    void ApplyChoice(float selectorY)
    {
        if(baseMenu.activeSelf)
        {
            if(inputManager.south_input) 
            {
                if(selectorY == continueY) playerManager.onOption = false;

                else if(selectorY == checkPointY) StartCoroutine (reloadRoutine());
                else if(selectorY == commandesY)
                {
                    baseMenu.SetActive(false);
                    buttonMap.SetActive(true);
                }
                else if(selectorY == quitY) Application.Quit();
            }
            else if(inputManager.lowAttack_input) playerManager.onOption = false;
        }
        else if(buttonMap.activeSelf && inputManager.lowAttack_input)  
        {
            buttonMap.SetActive(false);
            baseMenu.SetActive(true);
        }
        

    }

    IEnumerator reloadRoutine()
    {
        playerManager.onOption = false;

        inputManager.GetComponent<AudioManager>().jiataAudioSource.Stop();
        
        string filePath = Application.persistentDataPath + "/playerData.json";

        if(System.IO.File.Exists(filePath))
        {
            gameManager.newGame = 0;
            gameSaveManager.LoadAllData();
            float activeScene = SceneManager.GetActiveScene().buildIndex;
            if(activeScene == 2) gameSaveManager.LoadTorcheGrotteData();
            
            yield return new WaitForSeconds (0.5f);
            playerManager.onOption = false;
            this.gameObject.SetActive(false);
        }
    }

}
