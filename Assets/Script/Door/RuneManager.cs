using UnityEngine;
using SJ;

public class RuneManager : MonoBehaviour
{

    AudioManager audioManager;
    public RuneData runeData;
    RuneType runeType = new RuneType();
    public Material onRuneH, onRuneB;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(this.transform.parent.name == "Base Door") runeType.HandleBaseDoorRune(other, this);
        else if(this.transform.parent.name == "Mid Door") runeType.HandleMidDoorRune(other, this);
        else if(this.transform.parent.name == "Sup Door") runeType.HandleSupDoorRune(other, this);
        Debug.Log(runeData.base_DoorH);
    }
}

public class RuneType
{
    public void HandleBaseDoorRune(Collider other, RuneManager runeManager)
    {
        if(other.gameObject.layer == 8)
        {
            if(runeManager.gameObject.tag == other.gameObject.tag)
            {
                if(other.gameObject.tag == "RuneH" && !runeManager.runeData.base_DoorH)
                {
                    Debug.Log("Rune de base Active");
                    runeManager.runeData.base_DoorH = true;
                    runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneH;
                }
            }
        }        
    }

    public void HandleMidDoorRune(Collider other, RuneManager runeManager)
    {

        if(other.gameObject.layer == 8)
        {
            if(runeManager.gameObject.tag == other.gameObject.tag)
            {
                if(other.gameObject.tag == "RuneH" && !runeManager.runeData.mid_DoorH)
                {
                    //Debug.Log("Rune de base Active");
                    runeManager.runeData.mid_DoorH = true;
                    runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneH;

                }
                else if(other.gameObject.tag == "RuneB" && !runeManager.runeData.mid_DoorB)
                {
                    //Debug.Log("Rune de base Active");
                    runeManager.runeData.mid_DoorB = true;
                    runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneB;
                }
            }
        }

    }

    public void HandleSupDoorRune(Collider other, RuneManager runeManager)
        {

            if(other.gameObject.layer == 8)
            {
                if(runeManager.gameObject.tag == other.gameObject.tag)
                {
                    if(other.gameObject.tag == "RuneH" && !runeManager.runeData.sup_DoorH)
                    {
                        runeManager.runeData.sup_DoorH = true;
                        runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneH;
                    }
                    else if(other.gameObject.tag == "RuneB" && !runeManager.runeData.sup_DoorB)
                    {
                        runeManager.runeData.sup_DoorH = true;
                        runeManager.transform.gameObject.GetComponent<Renderer>().material = runeManager.onRuneB;
                    }
                }
            }

        }
}