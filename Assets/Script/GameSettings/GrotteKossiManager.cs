using UnityEngine;
using SJ;

[DefaultExecutionOrder(-2)]
public class GrotteKossiManager : MonoBehaviour
{
    public GameObject enemySpawnOne, enemySpawntwo, enemySpawnthree, enemySpawnFour;
    PlayerManager playerManager;
    GameSaveManager gameSaveManager;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
    }
    

    void Start()
    {
        enemySpawnOne.SetActive(playerManager.canSurcharge);
        if(gameSaveManager.midDoor.transform.position.y > 13)
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
