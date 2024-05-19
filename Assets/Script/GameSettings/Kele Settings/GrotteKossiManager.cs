using UnityEngine;
using SJ;

[DefaultExecutionOrder(-2)]
public class GrotteKossiManager : MonoBehaviour
{
    public GameObject enemySpawnOne, enemySpawntwo, enemySpawnthree, enemySpawnFour, kossiPortal;
    BaseDoorManager baseDoorManager;
    PlayerManager playerManager;
    GameSaveManager gameSaveManager;
    GameObject midDoorDownGO;

    
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        midDoorDownGO = FindObjectOfType<MidDoorManager>().gameObject.transform.GetChild(1).gameObject;
        baseDoorManager = FindObjectOfType<BaseDoorManager>();

        StoryManager storyManager;
        storyManager = FindObjectOfType<StoryManager>();

        if(storyManager.storyStep > 5) kossiPortal.SetActive(true);
        else kossiPortal.SetActive(false);

        gameSaveManager.HandleGrotteKossiDoor();

        if(gameSaveManager.isloaded)
        {

            gameSaveManager.LoadGrotteData();
            gameSaveManager.LoadTorcheGrotteData();          
        }

        enemySpawnOne.SetActive(playerManager.haveGauntlet);

        if(enemySpawnOne.activeSelf) baseDoorManager.runeDooBManager.LoadStateBaseRune();
        
        if(midDoorDownGO.transform.position.y > 59.3f)
        {
            enemySpawntwo.SetActive(true);
        }
        else enemySpawntwo.SetActive(false);
        
       // GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        //sun.transform.rotation = Quaternion.identity;
       // Quaternion rotation = Quaternion.Euler(-30f, 0f, 0f);
        //sun.transform.rotation = rotation;          

    }
}
