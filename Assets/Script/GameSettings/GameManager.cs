using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SJ;
using TMPro;


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
    [SerializeField] GameObject zoneName, questNotif;
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
        }
 
        else if(newGame == 1) 
        {
            player.transform.position = new Vector3 (132.10f, 3.50f, 355.91f); //position de ruben en tout debut de partie
            player.transform.rotation = Quaternion.Euler(0, -10.22f, 0f);
            player.GetComponent<AnimatorManager>().PlayTargetAnimation("Start", true);
        }
        else
        {
            if(activeScene == 1)
            {
                if(portalPosition == 0)
                {
                    player.transform.position = new Vector3 (50f, 10f, 307f);
                    GlobalFixedCursorPosition();
                } 
                else player.transform.position = new Vector3 (328.76f,40.09f,-179.47f);
            }
            else if(activeScene == 2)
            {
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

        yield return new WaitForSeconds(2f);

        if(activeScene == 1)
        {
            if(newGame == 1) StartCoroutine(ZoneEntry("...CASE DE LA TORTUE..."));
            else if(newGame == 0 || newGame == null)StartCoroutine(ZoneEntry("...SIBONGO..."));
        }
        else if(activeScene == 2) StartCoroutine(ZoneEntry("...GROTTE BONGO..."));
        
        player.isInteracting = false;
        Debug.Log("index est " + newGame);        
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
        CharacterManager [] characterManagers = new CharacterManager [7];
        characterManagers = FindObjectsOfType<CharacterManager>();
        foreach (var elt in characterManagers)
        {
            elt.FixedCursorPosition();
        }
    }

    public IEnumerator ZoneEntry(string nameZone)
    {
        zoneName.SetActive(true);
        zoneName.GetComponent<TextMeshProUGUI>().text = nameZone;
        yield return new WaitForSeconds(7.2f);
        zoneName.SetActive(false);
    }

    public IEnumerator StartHandleAchievement(string questName)
    {
        yield return new WaitForSeconds(2f);
        questNotif.SetActive(true);
        questNotif.transform.GetComponentInChildren<TextMeshProUGUI>().text = questName;
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<AudioManager>().PowerUp();
    }
}
