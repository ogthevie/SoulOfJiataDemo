using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {private set; get; }
    public Canvas loadingScreen;
    public Image loadSlider;


    private void Awake()
    {
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
