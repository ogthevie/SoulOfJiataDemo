using UnityEngine;

public class MidDoorManager : DoorManager
{
    public GameObject midDoorDown, wall;
    GrotteKossiManager grotteKossiManager;

    void Awake()
    {
        openPosition = midDoorDown.transform.position + new Vector3 (0, 8f, 0);
        stopPosition = openPosition - Vector3.up*2;
        doorAudioSource = GetComponent<AudioSource>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        grotteKossiManager = FindObjectOfType<GrotteKossiManager>();
    }

    void Start()
    {
        if(grotteKossiManager.enemySpawntwo.activeSelf)
        {
            Destroy(wall);
        }
    }


    private void LateUpdate() 
    {
        doorType.HandleMidDoor(runeData, midDoorDown, wall, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {

        if(mid_Door)  
        {
            gameSaveManager.SaveAllData();
            mid_Door = false;
            grotteKossiManager.enemySpawntwo.SetActive(true);
            if(wall != null) Destroy(wall);
            Destroy(this);
        }
    }
}
