using UnityEngine;

public class MidDoorManager : DoorManager
{
    public GameObject midDoorDown, wall;
    Vector3 stopMidDoorDownPosition;

    void Awake()
    {
        openPosition = midDoorDown.transform.position + new Vector3 (0, 8f, 0);
        stopPosition = openPosition - Vector3.up*2;
        doorAudioSource = GetComponent<AudioSource>();
    }


    private void LateUpdate() 
    {
        doorType.HandleMidDoor(runeData, midDoorDown, wall, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {
        if(mid_Door)  Destroy(this);
    }
}
