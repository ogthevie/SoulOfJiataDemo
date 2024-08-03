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
    RectTransform selector;
    [SerializeField] AudioClip [] selectorSfx = new AudioClip [2];
    float continueY, newGameY, quitY;
    bool up_input, down_input, south_input, pause_input, skip_input;
    bool isPlaying;
    string filePath;

    [SerializeField] GameObject introPlane, wind, buttonIcon, keyboardIcon;
    [SerializeField] Canvas titleGame;


    void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.InventoryMovementUp.performed += i => up_input =  true;
            playerControls.PlayerMovement.InventoryMovementDown.performed += i => down_input =  true;
            playerControls.PlayerActions.Jump.performed += i => south_input = true;
            playerControls.PlayerActions.OnPause.performed += i => pause_input = true;
            playerControls.PlayerActions.Option.performed += i => skip_input = true;            
        }
        playerControls.Enable();
        
    }

    private void OnDisable()
    {
        playerControls.Disable();
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
        filePath = Application.persistentDataPath + "/playerPosition.json";
    }

    void LateUpdate()
    {
        if(introPlane == null) return;
        if(!introPlane.activeSelf)
        {
            MoveSelector();
            ApplyChoice(selector.anchoredPosition.y);
        }

        HandlePauseAndSkip();
        up_input = false;
        down_input = false;
        south_input = false;
        skip_input =  false;
        if(gameManager.isControllerConnected)
        {
            buttonIcon.SetActive(true);
            keyboardIcon.SetActive(false);
        }
        else
        {
            buttonIcon.SetActive(false);
            keyboardIcon.SetActive(true);
        }
    }

    void MoveSelector()
    {
        if(up_input)
        {
            if(selector.anchoredPosition.y == continueY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, quitY);
            else if(selector.anchoredPosition.y == newGameY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, continueY);
            else if(selector.anchoredPosition.y == quitY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, newGameY);
            selectorAudioSource.PlayOneShot(selectorSfx[0]);
        }
        else if(down_input)
        {
            if(selector.anchoredPosition.y == continueY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, newGameY);
            else if(selector.anchoredPosition.y == newGameY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, quitY);
            else if(selector.anchoredPosition.y == quitY) selector.anchoredPosition = new Vector2 (selector.anchoredPosition.x, continueY);            
            selectorAudioSource.PlayOneShot(selectorSfx[0]);
        }
    }

    void ApplyChoice(float selectorY)
    {
        if(south_input || pause_input) 
        {
            if(selectorY == quitY) Application.Quit();
            else if(selectorY == continueY) 
            {
                if(gameManager.isLoading) return;
                if(System.IO.File.Exists(filePath))
                {
                    sceneAudioSource.Stop();
                    gameManager.ActiveOnDestroy();
                    gameManager.newGame = 0;  
                    gameSaveManager.LoadAllData();
                }
                else selectorAudioSource.PlayOneShot(selectorSfx[1]);

            }
            else if(selectorY == newGameY) 
            {
                if(gameManager.isLoading) return;
                gameSaveManager.ClearAllSaves();
                introPlane.SetActive(true);
                wind.SetActive(false);
                titleGame.enabled = false;
                sceneAudioSource.Stop();
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
        yield return new WaitForSeconds(0.05f);
        gameManager.LoadScene(sceneName);
    }

    void HandlePauseAndSkip()
    {
        if(skip_input)
        {
            if(gameManager.isLoading) return;
            LoadFirstScene();
            Destroy(introPlane, 0.5f);
        }
    } 
    void LoadFirstScene()
    {
        gameManager.ActiveOnDestroy();
        gameManager.newGame = 1;
        StartCoroutine(StartLoadingScene("Sibongo"));  
    }
}

