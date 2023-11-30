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

    void Start()
    {
        stelesGreenOn = stelesYellowOn = false;
        HandleHeartSteleActivationWithIndex();
        HandleTorcheLight();
    }


    void LateUpdate()
    {
        HandleTorcheLight();
        HandleArcLightActivation();
        HandleStele();
    }
    private void HandleTorcheLight()
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
                this.GetComponent<AudioSource>().enabled = true;
            }
            else if(stelesYellowOn)
            {
                this.GetComponent<Renderer>().material = altarYellowmat;
            }
            else if(!stelesGreenOn)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                this.GetComponent<Renderer>().material = altarOriginMat;
            }
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
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3 && !playerManager.canArcLight)
        {
            StartCoroutine(arcLightEvent());
        }        
    }

    IEnumerator arcLightEvent()
    {
        animatorManager.PlayTargetAnimation("PowerUp", true);
        playerManager.canArcLight = true;

        this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();

        Destroy(this.transform.GetChild(0).gameObject, 5f);
        yield return new WaitForSeconds(3f);
        this.GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(4f);
        Invoke("Save", 2f);
        
    }

    public void HandleHeartSteleActivationWithIndex()
    {
        for(int i = 0; i < 8; i++)
        {
            if(IndexHeartSteles[i] == 1) HeartSteles[i].GetComponent<Renderer>().material = refGreenMaterial;
            else if (IndexHeartSteles[i] == 2) HeartSteles[i].GetComponent<Renderer>().material = refYellowmaterial;
        }
    }
}
