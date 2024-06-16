using UnityEngine;
using SJ;

[DefaultExecutionOrder(-2)]
public class GrotteKossiManager : MonoBehaviour
{
    public GameObject enemySpawnFour, kossiPortal;
    BaseDoorManager baseDoorManager;
    PlayerManager playerManager;
    GameSaveManager gameSaveManager;

    
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        baseDoorManager = FindObjectOfType<BaseDoorManager>();

        StoryManager storyManager;
        storyManager = FindObjectOfType<StoryManager>();

        if(storyManager.storyStep > 5) kossiPortal.SetActive(true);
        else kossiPortal.SetActive(false);

        if(gameSaveManager.isloaded) gameSaveManager.LoadTorcheGrotteData();                  
    }
}
