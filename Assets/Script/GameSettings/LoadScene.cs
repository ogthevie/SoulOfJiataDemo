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
            if(portalName == "GolemPortal") gameManager.portalPosition = 1;
            else gameManager.portalPosition = 0;
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
