using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SJ;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    GameSaveManager gameSaveManager;
    public static GameManager Instance {private set; get; }
    public GameObject [] goDontDestroy = new GameObject [4];
    public Canvas loadingScreen, needGamepad;
    public Image loadSlider;
    public int? newGame;
    [SerializeField] bool isControllerConnected, isLoading;
    [SerializeField] GameObject minimap, minimapBG;
    public int? portalPosition;
    /*
        1 = golem
        0 = kossi
    */

    private void Awake()
    {
        gameSaveManager = GetComponent<GameSaveManager>();
        loadingScreen.enabled = false;
        needGamepad.enabled = false;
        isLoading = false; // a true de base
        //Shader.WarmupAllShaders();
    }

    public void ActiveOnDestroy()
    {
        float activeScene = SceneManager.GetActiveScene().buildIndex;
        
        {
            foreach(GameObject objPrefab in goDontDestroy)
            {
                objPrefab.SetActive(true);
            }
        }
    }

    void LateUpdate()
    {
        CheckForGamePad();
    }

    public void LoadScene(string scene)
    {
        List<AsyncOperation> operations = new List<AsyncOperation>();
        operations.Add(SceneManager.LoadSceneAsync(scene));
        StartCoroutine(Loading(operations));
    }

    private IEnumerator Loading(List<AsyncOperation> operations)
    {
        isLoading = true;

        GetComponent<DayNightCycleManager>().InitialiseDayTimer();

        for (int i = 0; i < operations.Count; i++)
        {
            while(!operations[i].isDone)
            {
                loadSlider.fillAmount = operations[i].progress;
                yield return null;
            }
        }

        float activeScene = SceneManager.GetActiveScene().buildIndex;
        PlayerManager player = FindObjectOfType<PlayerManager>();
        
        if(newGame == 0) 
        {
            gameSaveManager.LoadPlayerPosition();
            if(activeScene == 2)
            {
                minimap.SetActive(false);
                minimapBG.SetActive(false);               
            }   
        }
 
        else if(newGame == 1) 
        {
            player.transform.position = new Vector3 (128.04f, 4.99f, 337.64f); //position de ruben en tout debut de partie
            player.transform.rotation = Quaternion.Euler(0, -10.22f, 0f);
            player.GetComponent<AnimatorManager>().PlayTargetAnimation("Praying", true);
        }
        else
        {
            if(activeScene == 1)
            {
                minimap.SetActive(true);
                minimapBG.SetActive(true);
                if(portalPosition == 0) player.transform.position = new Vector3 (50f, 5f, 307f);
                else player.transform.position = new Vector3 (328.76f,40.09f,-179.47f);
            }
            else if(activeScene == 2)
            {
                minimap.SetActive(false);
                minimapBG.SetActive(false);
                if(portalPosition == 0) player.transform.position = new Vector3 (-124.38f, 46.3f, -179.057f);
                else player.transform.position = new Vector3 (-13.40f, 0.79f, 49.71f);

                gameSaveManager.LoadGrotteData();
                gameSaveManager.LoadTorcheGrotteData();

                portalPosition = null;
 
            }


        }

        yield return new WaitForSeconds(1f);

        loadingScreen.enabled = false;
        isLoading = false;
        
        player.isInteracting = false;        
    }

    private void CheckForGamePad()
    {
        if(isLoading) return;

        string[] joystickNames = Input.GetJoystickNames();

        if(joystickNames.Length == 0)
        {
            //Debug.Log("Manette déconnectée");
            needGamepad.enabled = true;
            Time.timeScale = 0;
            isControllerConnected = false;            
        }

        if (joystickNames.Length > 0 && !string.IsNullOrEmpty(joystickNames[0]))
        {
            if (!isControllerConnected)
            {
                //Debug.Log("Manette connectée : " + joystickNames[0]);
                needGamepad.enabled = false;
                Time.timeScale = 1;
                isControllerConnected = true;
            }
        }
        else
        {
            if (isControllerConnected)
            {
                //Debug.Log("Manette déconnectée");
                needGamepad.enabled = true;
                Time.timeScale = 0;
                isControllerConnected = false;
            }
        }
    }

    public void GlobalFixedCursorPosition()
    {
        CharacterManager [] characterManagers = new CharacterManager [6];
        characterManagers = FindObjectsOfType<CharacterManager>();
        foreach (var elt in characterManagers)
        {
            elt.FixedCursorPosition();
        }
    }
}
