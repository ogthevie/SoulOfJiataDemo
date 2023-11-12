using UnityEngine;

public class BaseDoorManager : DoorManager
{
    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        openPosition = this.transform.position + new Vector3 (0, 5.3f, 0);
        stopPosition = openPosition - Vector3.up*2;
    }

    private void LateUpdate() 
    {
        doorType.HandleBaseDoor(runeData, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {
        if(base_Door)
        {
            gameSaveManager.SaveGrotteData();
            Destroy(this);
    }
        }  
}
