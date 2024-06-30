using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    GameManager gameManager;
    public AudioSource sceneAudiosource;
    public string sceneName;
    string portalName;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneAudiosource = GameObject.FindWithTag("Respawn").GetComponent<AudioSource>();
        portalName = this.name;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.newGame = null;
            StartCoroutine(StartLoadingScene());
        }
    }

    public IEnumerator StartLoadingScene()
    {     
        gameManager.loadSlider.fillAmount = 0;
        sceneAudiosource.Stop();
        gameManager.loadingScreen.enabled = true;
        yield return new WaitForSeconds(0.15f);
        gameManager.LoadScene(sceneName);
    }
}
