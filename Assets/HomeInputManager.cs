#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine;

public class HomeInputManager : MonoBehaviour
{
    AudioSource selectorAudioSource, sceneAudioSource;
    PlayerControls playerControls;
    GameSaveManager gameSaveManager;
    GameManager gameManager;
    float continueY, newGameY, quitY;
    bool up_input, down_input, south_input;
    RectTransform selector;
    void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.InventoryMovementUp.performed += i => up_input =  true;
            playerControls.PlayerMovement.InventoryMovementDown.performed += i => down_input =  true;
            playerControls.PlayerActions.Jump.performed += i => south_input = true;            
        }
        playerControls.Enable();
    }

    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        gameManager = gameSaveManager.GetComponent<GameManager>();
        selectorAudioSource = GetComponent<AudioSource>();
        sceneAudioSource = GameObject.Find("JiataHome").GetComponent<AudioSource>();
        Transform parentGameobject =  transform.parent;
        continueY = parentGameobject.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;
        newGameY = parentGameobject.GetChild(1).GetComponent<RectTransform>().anchoredPosition.y;
        quitY = parentGameobject.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y;
        selector = GetComponent<RectTransform>();
        
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void LateUpdate()
    {
        MoveSelector();
        ApplyChoice(selector.anchoredPosition.y);
        up_input = false;
        down_input = false;
        south_input = false;
    }

    void MoveSelector()
    {
        if(up_input)
        {
            if(selector.anchoredPosition.y == continueY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, quitY);
            else if(selector.anchoredPosition.y == newGameY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, continueY);
            else if(selector.anchoredPosition.y == quitY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, newGameY);
            selectorAudioSource.Play();
        }
        else if(down_input)
        {
            if(selector.anchoredPosition.y == continueY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, newGameY);
            else if(selector.anchoredPosition.y == newGameY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, quitY);
            else if(selector.anchoredPosition.y == quitY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, continueY);            
            selectorAudioSource.Play();
        }
    }

    void ApplyChoice(float selectorY)
    {
        if(south_input) 
        {
            sceneAudioSource.Stop();
            if(selectorY == quitY) Application.Quit();
            else if(selectorY == continueY) 
            {
                if(!gameSaveManager.haveSave)
                {
                    selectorAudioSource.Play();
                }
                else
                {
                    gameManager.ActiveOnDestroy();
                    gameSaveManager.LoadAllData();                    
                }
            }
            else if(selectorY == newGameY) 
            {
                gameManager.ActiveOnDestroy();
                gameSaveManager.ClearAllSaves();
                StartCoroutine(StartLoadingScene("Sibongo"));
            }
        }

        #if UNITY_EDITOR
        // Si nous sommes dans l'éditeur, vérifions si nous devons quitter le mode lecture
        if (Input.GetKeyDown(KeyCode.Escape) || (south_input && selectorY == quitY))
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
        }
        #endif
    }


    public IEnumerator StartLoadingScene(string sceneName)
    {     
        gameManager.loadSlider.fillAmount = 0;
        gameManager.loadingScreen.enabled = true;
        yield return new WaitForSeconds(0.15f);
        gameManager.LoadScene(sceneName);
    }
}

