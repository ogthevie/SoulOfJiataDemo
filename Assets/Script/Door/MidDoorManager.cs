public class MidDoorManager : DoorManager
{
    private void LateUpdate() 
    {
        doorType.HandleMidDoor(runeData, this);
        HandleStopDoorRuneProcess();    
    }

    protected override void HandleStopDoorRuneProcess()
    {
        if(runeData.mid_Door)  Destroy(this);
    }
}
