using UnityEngine;
using SJ;

public class RuneManager : MonoBehaviour
{

    AudioSource audioSource;
    public RuneData runeData;
    RuneType runeType = new RuneType();
    public Material onRuneH, onRuneB;

    void Awake()
    {
        runeData.base_DoorB = runeData.mid_DoorB = runeData.mid_DoorH =
        runeData.sup_DoorB = runeData.sup_DoorH = false;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadStateBaseRune()
    {
        transform.GetComponent<Renderer>().material = onRuneB;
        Destroy(this, 5);
    }

    public void LoadStateGreenMidRune()
    {
        transform.GetComponent<Renderer>().material = onRuneH;
        Destroy(this, 5);
    }
    void OnTriggerEnter(Collider other)
    {
        if(this.transform.parent.name == "Base Door") runeType.HandleBaseDoorRune(other, this, audioSource);
        else if(this.transform.parent.name == "Mid Door") runeType.HandleMidDoorRune(other, this, audioSource);

        if(other.gameObject.layer == 12)
        {
            if(other.gameObject.TryGetComponent<kossiKazePattern>(out kossiKazePattern component))
            {
                component.HandleExplosion();
            }
        }
        //Debug.Log(runeData.base_DoorB);
    }
}

public class RuneType
{
    public void HandleBaseDoorRune(Collider other, RuneManager runeManager, AudioSource audioSource)
    {
        if(other.gameObject.layer == 8)
        {

            if(other.gameObject.transform.GetChild(1).gameObject.activeSelf && !runeManager.runeData.base_DoorB)
            {
                //Debug.Log("Rune de base Active");
                audioSource.Play();
                runeManager.runeData.base_DoorB = true;
                runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneB;
            }

        }        
    }

    public void HandleMidDoorRune(Collider other, RuneManager runeManager, AudioSource audioSource)
    {

        if(other.gameObject.layer == 8)
        {
            if(other.gameObject.transform.GetChild(0).gameObject.activeSelf && runeManager.gameObject.name == "RuneH" && !runeManager.runeData.mid_DoorH)
            {
                //Debug.Log("Rune de base Active");
                audioSource.Play();
                runeManager.runeData.mid_DoorH = true;
                runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneH;

            }
            else if(other.gameObject.transform.GetChild(1).gameObject.activeSelf && runeManager.gameObject.name == "RuneB" && !runeManager.runeData.mid_DoorB)
            {
                //Debug.Log("Rune de base Active");
                audioSource.Play();
                runeManager.runeData.mid_DoorB = true;
                runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneB;
            }
        }

    }
}
