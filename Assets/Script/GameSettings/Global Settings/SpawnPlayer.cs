using UnityEngine.SceneManagement;
using UnityEngine;
using SJ;
using System.Collections;

[DefaultExecutionOrder(3)]
public class SpawnPlayer : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        GameObject player = FindFirstObjectByType<PlayerManager>().gameObject;
        GameSaveManager gameSaveManager;
        gameManager = FindFirstObjectByType<GameManager>();
        gameSaveManager = FindFirstObjectByType<GameSaveManager>();
        if(!gameSaveManager.isloaded)
        {
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

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            StartCoroutine (deblock());
            player.transform.position = this.transform.position;
        } 
        
        gameSaveManager.isloaded = false;

        //A retirer
        IEnumerator deblock()
        {
            yield return new WaitForSeconds (2.5f);
            player.GetComponent<PlayerStats>().TakeDamage(1, 0);  
        }
        //Fin

    }
}
