using UnityEngine;
using SJ;

public class GrotteKossiManager : MonoBehaviour
{
    public GameObject enemySpawnOne, enemySpawntwo, enemySpawnthree, enemySpawnFour;
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    

    void Start()
    {
        if(playerManager.canSurcharge)
            enemySpawnOne.SetActive(true);
        else
            enemySpawnOne.SetActive(false);
    }
}
