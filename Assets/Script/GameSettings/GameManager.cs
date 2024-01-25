using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SJ;


//[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    GameSaveManager gameSaveManager;
    public static GameManager Instance {private set; get; }
    public GameObject [] goDontDestroy = new GameObject [4];
    public Canvas loadingScreen, needGamepad;
    public Image loadSlider;
    public int? newGame;
    [SerializeField] bool isControllerConnected, isLoading;

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
        
        if(newGame == 1) 
        {
            var playerTransform = FindObjectOfType<PlayerManager>().transform;
            playerTransform.position = new Vector3 (128.04f, 4.99f, 337.64f); //position de ruben en tout debut de partie
            playerTransform.rotation = Quaternion.Euler(0, -10.22f, 0f);
            playerTransform.GetComponent<AnimatorManager>().PlayTargetAnimation("Praying", true);
        }
        else if(newGame == 0)gameSaveManager.LoadPlayerPosition();
        else
        {
            if(activeScene == 1)FindObjectOfType<PlayerManager>().transform.position = new Vector3 (123.76f, 5.3f, 346.57f);
            else if(activeScene == 2)FindObjectOfType<PlayerManager>().transform.position = new Vector3 (-124.38f, 46.3f, -179.057f);
        }

        yield return new WaitForSeconds(0.5f);

        loadingScreen.enabled = false;
        isLoading = false;

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
