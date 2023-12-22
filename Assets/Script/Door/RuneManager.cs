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
            transform.gameObject.GetComponent<Renderer>().material = onRuneB;
            Destroy(this, 5);
    }


    void OnTriggerEnter(Collider other)
    {
        if(this.transform.parent.name == "Base Door") runeType.HandleBaseDoorRune(other, this, audioSource);
        else if(this.transform.parent.name == "Mid Door") runeType.HandleMidDoorRune(other, this, audioSource);
        else if(this.transform.parent.name == "Sup Door") runeType.HandleSupDoorRune(other, this, audioSource);

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
            if(other.gameObject.transform.GetChild(0).gameObject.activeSelf && !runeManager.runeData.mid_DoorH)
            {
                //Debug.Log("Rune de base Active");
                audioSource.Play();
                runeManager.runeData.mid_DoorH = true;
                runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneH;

            }
            else if(other.gameObject.transform.GetChild(1).gameObject.activeSelf && !runeManager.runeData.mid_DoorB)
            {
                //Debug.Log("Rune de base Active");
                audioSource.Play();
                runeManager.runeData.mid_DoorB = true;
                runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneB;
            }
        }

    }

    public void HandleSupDoorRune(Collider other, RuneManager runeManager, AudioSource audioSource)
        {

            if(runeManager.gameObject.tag == other.gameObject.tag)
            {
                if(other.gameObject.transform.GetChild(0).gameObject.activeSelf && !runeManager.runeData.sup_DoorH)
                {
                    runeManager.runeData.sup_DoorH = true;
                    audioSource.Play();
                    runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneH;
                }
                else if(other.gameObject.transform.GetChild(1).gameObject.activeSelf && !runeManager.runeData.sup_DoorB)
                {
                    runeManager.runeData.sup_DoorH = true;
                    audioSource.Play();
                    runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneB;
                }
            }

        }
}
