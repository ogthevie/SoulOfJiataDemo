using UnityEngine;

public class BaseDoorManager : DoorManager
{
    public RuneManager runeDooBManager;

    void Awake()
    {
        openPosition = this.transform.position + new Vector3 (0, 5.3f, 0);
        stopPosition = openPosition - Vector3.up*2;
        doorAudioSource = GetComponent<AudioSource>();
        gameSaveManager = FindObjectOfType<GameSaveManager>();

    }

    void Start()
    {
        if(transform.position.y >= stopPosition.y)
        {
            base_Door = true;
            runeDooBManager.LoadStateBaseRune();
        }

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
            gameSaveManager.SaveDoorData();
            Destroy(this, 2f);
        }
    }  
}
