using UnityEngine.SceneManagement;
using UnityEngine;
using SJ;

[DefaultExecutionOrder(2)]
public class SpawnPlayer : MonoBehaviour
{
    GameManager gameManager;
    bool checkLoadScreen;
    
    void Start()
    {
        checkLoadScreen = false;
        GameSaveManager gameSaveManager;
        gameManager = FindObjectOfType<GameManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        if(!gameSaveManager.isloaded)
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Quaternion targetRotation;
            targetRotation = Quaternion.Euler(0, 0, 0f);
            //else targetRotation = Quaternion.identity;
            player.transform.position = transform.position;
            player.transform.rotation *= targetRotation;       
        }
        else
        {
            gameSaveManager.LoadPlayerPosition();  
        }

        gameSaveManager.isloaded = false;

    }
    void LateUpdate()
    {
        if(!checkLoadScreen)
        {
            float activeScene = SceneManager.GetActiveScene().buildIndex;
            if(!gameManager.loadingScreen.enabled)
            {
                if(activeScene ==  1) FindObjectOfType<PlayerManager>().transform.position = new Vector3 (123.76f, 5.3f, 346.57f);
                else if(activeScene == 2) FindObjectOfType<PlayerManager>().transform.position = new Vector3 (-124.38f, 60, -179.057f);
                checkLoadScreen = true;
            }
        }
    }  
}
