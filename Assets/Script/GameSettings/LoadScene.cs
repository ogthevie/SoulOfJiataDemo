using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    GameManager gameManager;
    AudioSource sceneAudiosource;
    public string sceneName;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneAudiosource = GameObject.Find("SceneManager").GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(StartLoadingScene());
        }
    }

    public IEnumerator StartLoadingScene()
    {     
        gameManager.loadSlider.fillAmount = 0;
        gameManager.loadingScreen.enabled = true;
        sceneAudiosource.Stop();
        yield return new WaitForSeconds(0.15f);
        gameManager.LoadScene(sceneName);
    }
}
