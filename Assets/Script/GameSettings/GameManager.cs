using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SJ;
using TMPro;
using System;
using System.Linq;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    GameSaveManager gameSaveManager;
    public static GameManager Instance {private set; get; }
    public GameObject [] goDontDestroy = new GameObject [4];
    public Canvas loadingScreen, needGamepad;
    public Image loadSlider;
    public int? newGame;
    public bool isControllerConnected, isLoading, canNotif;
    [SerializeField] GameObject zoneName, questNotif, completeNotif;
    [SerializeField] List <GameObject> buttonIcons = new List<GameObject>();
    [SerializeField] List <GameObject> keyBoardIcons = new List<GameObject>();
    String [] questNames = new string [10];
    String [] questDescriptons = new string [10];

    private void Awake()
    {
        gameSaveManager = GetComponent<GameSaveManager>();
        loadingScreen.enabled = false;
        needGamepad.enabled = false;
        isLoading = false; // a true de base
        Shader.WarmupAllShaders();
        InitializeQuestName();
        InitializeQuestDescription();
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
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

    void InitializeQuestName()
    {
        questNames[0] = "le chant du vieux coq";
        questNames[1] = "le sommeil de la roche";
        questNames[2] = "Le temple de BONGO";
        questNames[3] = "Le second brassard";
        questNames[4] = "L'esprit de la roche";
        questNames[6] = "L'eveil de l'HOMME";
        questNames[7] = "Cap sur WONDO";
    }

    void InitializeQuestDescription()
    {
        questDescriptons[0] = "Parlez à Baba Nlomgan";
        questDescriptons[1] = "Récoltez des informations sur l'Homme dans la pierre";
        questDescriptons[2] = "Fouillez la grotte des kossi";
        questDescriptons[3] = "Retrouvez les autres fragments du second brassard";
        questDescriptons[4] = "Libérez l'esprit de la roche";
        questDescriptons[6] = "Parlez à l'Homme dans la pierre";
        questDescriptons[7] = "Le début du périple";
    }

    void LateUpdate()
    {
        CheckForGamePad();
    }

    public void LoadScene(string scene)
    {
        if(isLoading) return;
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
        PlayerManager player = FindObjectOfType<PlayerManager>();
        
        if(newGame == 0) 
        {
            gameSaveManager.LoadPlayerPosition(); 
        }
 
        else if(newGame == 1) 
        {
            player.transform.position = new Vector3 (132.10f, 1.5f, 355.91f); //position de ruben en tout debut de partie
            //player.transform.rotation = Quaternion.Euler(0, -10.22f, 0f);
            player.GetComponent<AnimatorManager>().PlayTargetAnimation("Start", true);
        }
        else
        {
            float activeScene = SceneManager.GetActiveScene().buildIndex;
            if(activeScene == 1)
            {
  
                player.transform.position = new Vector3 (132.10f, 1.5f, 355.91f);
                GlobalFixedCursorPosition();
            }
            else if(activeScene == 2)
            {

                player.transform.position = new Vector3 (-124.38f, 46.15f, -179.057f);
                
                gameSaveManager.LoadTorcheGrotteData();
                gameSaveManager.SaveAllData();
            }


        }

        yield return new WaitForSeconds(1f);

        loadingScreen.enabled = false;
        isLoading = false;

        yield return new WaitForSeconds(2f);

        StoryManager storyManager = FindObjectOfType<StoryManager>();

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(ZoneEntry("...SIBONGO...", "Lituba"));
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(ZoneEntry("...Grotte des Kossi...", "Bongo"));
        }

        if(newGame != 1)
        {
            canNotif = true;
            StartCoroutine(StartHandleToDo(storyManager.storyStep));
        } 
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
            //needGamepad.enabled = true;
            //Time.timeScale = 0;
            isControllerConnected = false;            
        }

        if (joystickNames.Length > 0 && !string.IsNullOrEmpty(joystickNames[0]))
        {
            if (!isControllerConnected)
            {
                //Debug.Log("Manette connectée : " + joystickNames[0]);
                //needGamepad.enabled = false;
                //Time.timeScale = 1;
                isControllerConnected = true;
                foreach (var buttonIcon in buttonIcons)
                {
                    if(buttonIcon != null) buttonIcon.SetActive(true);
                }
                foreach (var keyBoardIcon in keyBoardIcons)
                {
                    if(keyBoardIcon != null) keyBoardIcon.SetActive(false);
                }  
            }
        }
        else
        {
            if (isControllerConnected)
            {
                //Debug.Log("Manette déconnectée");
                //needGamepad.enabled = true;
                //Time.timeScale = 0;
                isControllerConnected = false;
                foreach (var buttonIcon in buttonIcons)
                {
                    buttonIcon.SetActive(false);
                }
                foreach (var keyBoardIcon in keyBoardIcons)
                {
                    keyBoardIcon.SetActive(true);
                }  
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

    public IEnumerator ZoneEntry(string nameZone,string timeDay)
    {
        zoneName.GetComponent<TextMeshProUGUI>().text = nameZone;
        zoneName.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = timeDay;
        zoneName.SetActive(true);
        yield return new WaitForSeconds(7.2f);
        zoneName.SetActive(false);
    }

    public IEnumerator StartHandleAchievement(string completeName)
    {
        yield return new WaitForSeconds(2f);
        completeNotif.SetActive(true);
        completeNotif.transform.GetComponentInChildren<TextMeshProUGUI>().text = completeName;
        yield return new WaitForSeconds (8.5f);
        completeNotif.SetActive(false);
    }

    public IEnumerator StartHandleToDo(int i)
    {
        yield return new WaitForSeconds(1f);
        if(i >= 0 && i < questNames.Count() && canNotif)
        {
            questNotif.SetActive(true);
            questNotif.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questNames[i];
            questNotif.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questDescriptons[i];
        }
        yield return new WaitForSeconds (13f);
        questNotif.SetActive(false);
        canNotif = false;
    }
}
