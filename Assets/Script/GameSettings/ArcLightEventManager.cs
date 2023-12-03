using System.Linq;
using System.Collections;
using UnityEngine;

public class ArcLightEventManager : EventStoryTriggerManager
{
    public Material altarOriginMat, altarGreenMat, altarYellowmat, refGreenMaterial, refYellowmaterial, materialHeart;
    public GameObject [] HeartSteles = new GameObject [8];
    public GameObject [] Torche = new GameObject [8];
    public int [] IndexHeartSteles = new int [8];
    bool stelesGreenOn, stelesYellowOn;
    public bool inSecteurFour;

    void Start()
    {
        inSecteurFour = false;
        stelesGreenOn = stelesYellowOn = false;
        HandleHeartSteleActivationWithIndex();
        HandleBaliseLight();
    }


    void LateUpdate()
    {
        if(inSecteurFour)
        {
            HandleBaliseLight();
            HandleArcLightActivation();
            HandleForceKossiActivation();
            HandleStele();
        }

    }
    private void HandleBaliseLight()
    {
        int i;
        for(i = 0; i <= 7; i++)
        {
            materialHeart = HeartSteles[i].GetComponent<Renderer>().sharedMaterial;
            if(materialHeart == refGreenMaterial && !Torche[i].activeSelf) 
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

            if(stelesGreenOn)
            {
                this.GetComponent<Renderer>().material = altarGreenMat;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                this.GetComponent<AudioSource>().enabled = true;
            }
            else if(stelesYellowOn)
            {
                this.GetComponent<Renderer>().material = altarYellowmat;
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
                this.GetComponent<AudioSource>().enabled = false;
            }
            else if(!stelesGreenOn)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                this.GetComponent<Renderer>().material = altarOriginMat;
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

            if(endLoop) stelesYellowOn = true;
        }
    }

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
            transform.GetChild(0).gameObject.SetActive(false);
            this.GetComponent<AudioSource>().Stop();

        }
        if(playerManager.canBaemb) transform.GetChild(1).gameObject.SetActive(false);


        //H
    }

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

        this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();

        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        this.GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(4f);
        Invoke("Save", 5f);
        
    }

    IEnumerator ForceKossiEvent()
    {
        animatorManager.PlayTargetAnimation("PowerUp Baemb", true);
        playerManager.canBaemb = true;

        this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();

        this.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        this.GetComponent<AudioSource>().enabled = false;
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
}
