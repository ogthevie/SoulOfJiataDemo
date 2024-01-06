using UnityEngine;
using SJ;

[DefaultExecutionOrder(-2)]
public class GrotteKossiManager : MonoBehaviour
{
    public GameObject enemySpawnOne, enemySpawntwo, enemySpawnthree, enemySpawnFour;
    PlayerManager playerManager;
    GameSaveManager gameSaveManager;
    GameObject midDoorDownGO;

    
    void Start()
    {
        FindObjectOfType<PlayerUIManager>().transform.GetChild(4).gameObject.SetActive(false);
        playerManager = FindObjectOfType<PlayerManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        midDoorDownGO = FindObjectOfType<MidDoorManager>().gameObject.transform.GetChild(1).gameObject;

        gameSaveManager.HandleGrotteKossiDoor();

        if(gameSaveManager.isloaded)
        {

            gameSaveManager.LoadGrotteData();
            gameSaveManager.LoadTorcheGrotteData();          
        }

        enemySpawnOne.SetActive(playerManager.canSurcharge);
        if(midDoorDownGO.transform.position.y > 59.3f)
        {
            enemySpawntwo.SetActive(true);
        }
        else enemySpawntwo.SetActive(false);
        
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        sun.transform.rotation = Quaternion.identity;
        Quaternion rotation = Quaternion.Euler(-30f, 0f, 0f);
        sun.transform.rotation = rotation;          

    }
}
