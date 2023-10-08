
public class BaseDoorManager : DoorManager
{
    private void LateUpdate() 
    {
        doorType.HandleBaseDoor(runeData, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {
        if(runeData.base_Door)  Destroy(this);
    }
}
