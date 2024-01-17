using UnityEngine.SceneManagement;
using UnityEngine;
using SJ;

[DefaultExecutionOrder(2)]
public class SpawnPlayer : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
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
}
