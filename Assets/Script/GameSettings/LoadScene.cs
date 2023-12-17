using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    GameManager gameManager;
    public string sceneName;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(StartLoadingScene());
        }
    }

    IEnumerator StartLoadingScene()
    {     
        gameManager.loadSlider.fillAmount = 0;
        gameManager.loadingScreen.enabled = true;
        yield return new WaitForSeconds(0.15f);
        gameManager.LoadScene(sceneName);
    }
}
