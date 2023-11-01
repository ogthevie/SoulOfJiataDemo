using UnityEngine;

public abstract class DoorManager : MonoBehaviour
{
    public RuneData runeData;
    public AudioSource doorAudioSource;
    protected DoorType doorType = new DoorType();
    public Vector3 openPosition;
    public Vector3 stopPosition;


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

    public void HandleMidDoor (RuneData runeData, GameObject midDoorDown, GameObject wall, DoorManager doorManager)
    {
        if(!runeData.mid_Door)
        {

            if(runeData.mid_DoorH && runeData.mid_DoorB)
            {
                float velocity = 0.1f;
                doorManager.doorAudioSource.enabled = true;
                midDoorDown.transform.position = Vector3.Lerp(midDoorDown.transform.position, doorManager.openPosition, velocity * Time.deltaTime);
                wall.SetActive(false);
            }

            if(midDoorDown.transform.position.y > doorManager.stopPosition.y)
            {
                runeData.mid_Door = true;
            }

        }
    }

    public void HandleSupDoor (RuneData runeData, GameObject supDoorDown, DoorManager doorManager)
    {
        if(!runeData.sup_Door)
        {
            if(runeData.sup_DoorH && runeData.sup_DoorB && runeData.sup_DoorG)
            {
                float velocity = 0.2f;
                doorManager.transform.position = Vector3.Lerp(supDoorDown.transform.position, doorManager.openPosition, velocity * Time.deltaTime);
            }

            if(supDoorDown.transform.position.y > doorManager.stopPosition.y)
            {
                runeData.sup_Door = true;
            }  
        }
    }
}
