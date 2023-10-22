using UnityEngine;

public abstract class DoorManager : MonoBehaviour
{
    public RuneData runeData;
    protected DoorType doorType = new DoorType();
    public Vector3 openPosition;
    public Vector3 stopPosition;


    void Awake()
    {
        openPosition = this.transform.position + new Vector3 (0, 10, 0);
        stopPosition = openPosition - Vector3.up*2;
    }

    protected abstract void HandleStopDoorRuneProcess();


}

public class DoorType
{
    public void HandleBaseDoor(RuneData runeData, DoorManager doorManager)
    {
        if(!runeData.base_Door)
        {
            if(runeData.base_DoorB)
            {
                float velocity = 0.2f;
                doorManager.transform.position = Vector3.Lerp(doorManager.transform.position, doorManager.openPosition, velocity * Time.deltaTime);
            }

            if(doorManager.transform.position.y > doorManager.stopPosition.y) 
            {
                runeData.base_Door = true;
            }
        }
    }

    public void HandleMidDoor (RuneData runeData, DoorManager doorManager)
    {
        if(!runeData.mid_Door)
        {
            Vector3 openPosition = doorManager.transform.position + new Vector3 (0, 10, 0);

            if(runeData.mid_DoorH && runeData.mid_DoorB)
            {
                float velocity = 0.2f;
                doorManager.transform.position = Vector3.Lerp(doorManager.transform.position, openPosition, velocity * Time.deltaTime);
            }

            if(doorManager.transform.position.y > doorManager.stopPosition.y)
            {
                runeData.mid_Door = true;
            }

        }
    }

    public void HandleSupDoor (RuneData runeData, DoorManager doorManager)
    {
        if(!runeData.sup_Door)
        {
            Vector3 openPosition = doorManager.transform.position + new Vector3 (0, 10, 0);

            if(runeData.sup_DoorH && runeData.sup_DoorB && runeData.sup_DoorG)
            {
                float velocity = 0.2f;
                doorManager.transform.position = Vector3.Lerp(doorManager.transform.position, openPosition, velocity * Time.deltaTime);
            }

            if(doorManager.transform.position.y > doorManager.stopPosition.y)
            {
                runeData.sup_Door = true;
            }  
        }
    }
}
