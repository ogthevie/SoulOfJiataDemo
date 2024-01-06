using System.Linq;
using System.Collections;
using UnityEngine;

public class ArcLightEventManager : EventStoryTriggerManager
{
    BigKossiEventManager bigKossiEventManager;
    public Material altarOriginMat, altarGreenMat, altarYellowmat, refGreenMaterial, refYellowmaterial, materialHeart;
    public GameObject [] HeartSteles = new GameObject [8];
    public GameObject [] Torche = new GameObject [8];
    public int [] IndexHeartSteles = new int [8];
    bool stelesGreenOn, stelesYellowOn;
    public GameObject arcLight, forceKossi;
    public bool inSecteurFour;

    void Start()
    {
        bigKossiEventManager = FindObjectOfType<BigKossiEventManager>();
        inSecteurFour = false;
        stelesGreenOn = stelesYellowOn = false;
        arcLight = transform.GetChild(0).gameObject;
        forceKossi = transform.GetChild(1).gameObject;
        HandleHeartSteleActivationWithIndex();
        HandleBaliseLight();
    }

    void Update()
    {
        HandleBaliseLight();
    }

    void LateUpdate()
    {
        //HandleActivationBigKossiEvent();
        
        if(inSecteurFour)
        {
            if(playerManager.canArcLight && playerManager.canBaemb)
            return;

            HandleArcLightActivation();
            HandleForceKossiActivation();
            HandleStele();
        }

    }
    private void HandleBaliseLight() //creer un evenement
    {

        int i;
        for(i = 0; i < HeartSteles.Length; i++)
        {
            materialHeart = HeartSteles[i].GetComponent<Renderer>().sharedMaterial;
            if(materialHeart == refGreenMaterial) 
            {
                Torche[i].SetActive(true);
                IndexHeartSteles[i] = 1;
            }
            else if(materialHeart != refGreenMaterial) 
            {
                stelesGreenOn = false;
                Torche[i].SetActive(false);
                if(materialHeart == refYellowmaterial) IndexHeartSteles[i] = 2;
                else IndexHeartSteles[i] = 0;
            }

        }
    }

    void HandleArcLightActivation()
    {
        if(stelesGreenOn == false)
        {
            foreach (GameObject torche in Torche) 
            {
                if (!torche.activeSelf) 
                {
                    stelesGreenOn = false;
                    break;
                }
            }

            stelesGreenOn = true && Torche.All(t => t.activeSelf);

            if(stelesGreenOn && !arcLight.activeSelf)
            {
                GetComponent<Renderer>().material = altarGreenMat;
                arcLight.SetActive(true);
                forceKossi.SetActive(false);
                GetComponent<AudioSource>().enabled = true;
            }
            else if(stelesYellowOn)
            {
                GetComponent<Renderer>().material = altarYellowmat;
                forceKossi.SetActive(true);
                arcLight.SetActive(false);
                GetComponent<AudioSource>().enabled = false;
            }
            else if(!stelesGreenOn)
            {
                arcLight.SetActive(false);
                GetComponent<Renderer>().material = altarOriginMat;
                GetComponent<AudioSource>().enabled = false;
            }
        }

    }

    void HandleForceKossiActivation()
    {
        bool endLoop = true;

        if(!stelesYellowOn)
        {
            foreach(GameObject heartStele in HeartSteles)
            {
                materialHeart = heartStele.GetComponent<Renderer>().sharedMaterial;
                if(materialHeart != refYellowmaterial) 
                {
                    endLoop = false;
                    break;
                }
            }
        }

        Debug.Log(endLoop);

        stelesYellowOn = endLoop;
    }

    //////////////////////////////////////////A RETIRER ////////////////////////////////////////////////////////////////

    public void HandleStele()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            foreach(GameObject steles in HeartSteles)
            {
                steles.GetComponent<Renderer>().material = refGreenMaterial;
            }
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            foreach(GameObject steles in HeartSteles)
            {
                steles.GetComponent<Renderer>().material = refYellowmaterial;
            }            
        }

        if(playerManager.canArcLight) 
        {
            arcLight.SetActive(false);
            GetComponent<AudioSource>().Stop();

        }
        if(playerManager.canBaemb) forceKossi.SetActive(false);


    }

    ///////////////////////////////////////////////        FIN A RETIRER    ////////////////////////////////////////////////////////////////////////////

    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3 && stelesGreenOn)
        {
            if(playerManager.canArcLight ) return;
            inSecteurFour = true;
            StartCoroutine(arcLightEvent());
        }
        else if(other.gameObject.layer == 3 && stelesYellowOn)
        {
            if(playerManager.canBaemb) return;
            inSecteurFour = true;
            StartCoroutine(ForceKossiEvent());
        }
    }

    IEnumerator arcLightEvent()
    {
        animatorManager.PlayTargetAnimation("PowerUp", true);
        playerManager.canArcLight = true;

        arcLight.GetComponent<ParticleSystem>().Stop();

        arcLight.SetActive(false);
        yield return new WaitForSeconds(3f);
        GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(4f);
        Invoke("Save", 5f);
        
    }

    IEnumerator ForceKossiEvent()
    {
        animatorManager.PlayTargetAnimation("PowerUp Baemb", true);
        playerManager.canBaemb = true;

        forceKossi.GetComponent<ParticleSystem>().Stop();

        forceKossi.SetActive(false);
        yield return new WaitForSeconds(3f);
        GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(4f);
        Invoke("Save", 5f);
        
    }

    //corroutine forcekossievent

    public void HandleHeartSteleActivationWithIndex()
    {
        for(int i = 0; i < 8; i++)
        {
            if(IndexHeartSteles[i] == 1) HeartSteles[i].GetComponent<Renderer>().material = refGreenMaterial;
            else if (IndexHeartSteles[i] == 2) HeartSteles[i].GetComponent<Renderer>().material = refYellowmaterial;
        }
    }

    public void HandleActivationBigKossiEvent()
    {
        if(bigKossiEventManager.enabled)
            return; 
        if(playerManager.canArcLight && playerManager.canBaemb)
        {
            bigKossiEventManager.enabled = true;
            storyManager.storyStep = 6;
            foreach(GameObject steles in HeartSteles)
            {
                steles.GetComponent<Renderer>().material = altarOriginMat;
            }
        }
    }
}
