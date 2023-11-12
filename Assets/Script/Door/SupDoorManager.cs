using UnityEngine;

public class SupDoorManager : DoorManager
{
    public GameObject supDoor;
    Vector3 stopSupDoorposition;

    private void LateUpdate() 
    {
        doorType.HandleSupDoor(runeData, supDoor, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {
        if(sup_Door)  Destroy(this);
    }
}
