using UnityEngine;
using SJ;

[DefaultExecutionOrder(-2)]
public class GrotteKossiManager : MonoBehaviour
{
    public GameObject enemySpawnFour, kossiPortal;
    public SteleManager [] steleManagers = new SteleManager[3];
    GameSaveManager gameSaveManager;

    
    void Start()
    {
        gameSaveManager = FindFirstObjectByType<GameSaveManager>();

        StoryManager storyManager;
        storyManager = FindFirstObjectByType<StoryManager>();

        if(storyManager.storyStep > 5) kossiPortal.SetActive(true);
        else kossiPortal.SetActive(false);

        if(gameSaveManager.isloaded) gameSaveManager.LoadSteleGrotteData();                  
    }
}
