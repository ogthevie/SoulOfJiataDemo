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
    public Canvas loadingScreen;
    public Image loadSlider;

    private void Awake()
    {
        gameSaveManager = GetComponent<GameSaveManager>();
        /*foreach(GameObject elt in goDontDestroy)
        {
            elt.SetActive(false);
        }*/
        loadingScreen = GetComponentInChildren<Canvas>();
        loadingScreen.enabled = false;
        /*if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        */
        //SceneManager.activeSceneChanged += ActiveOnDestroy;
    }

    public void ActiveOnDestroy()
    {
        float activeScene = SceneManager.GetActiveScene().buildIndex;
        
        //if(activeScene != 0)
        {
            foreach(GameObject objPrefab in goDontDestroy)
            {
                objPrefab.SetActive(true);
            }

            //SceneManager.activeSceneChanged -= ActiveOnDestroy;
        }
    }

    public void LoadScene(string scene)
    {
        List<AsyncOperation> operations = new List<AsyncOperation>();
        operations.Add(SceneManager.LoadSceneAsync(scene));
        StartCoroutine(Loading(operations));


    }

    private IEnumerator Loading(List<AsyncOperation> operations)
    {

        for (int i = 0; i < operations.Count; i++)
        {
            while(!operations[i].isDone)
            {
                loadSlider.fillAmount = operations[i].progress;
                yield return null;
            }
        }
        yield return new WaitForSeconds(1.5f);

        loadingScreen.enabled = false;

    }
}
