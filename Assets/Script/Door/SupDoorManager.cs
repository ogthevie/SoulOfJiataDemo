
public class SupDoorManager : DoorManager
{
    private void LateUpdate() 
    {
        doorType.HandleSupDoor(runeData, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {
        if(runeData.sup_Door)  Destroy(this);
    }
}
