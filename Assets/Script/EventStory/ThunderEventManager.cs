using System.Linq;
using System.Collections;
using UnityEngine;
using SJ;

public class ThunderEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    BuffaloManager buffaloManager;
    public Material altarOriginMat, altarGreenMat, altarYellowmat, refGreenMaterial, refYellowmaterial, materialHeart;
    public AudioSource bossThemeAudioSource;
    public GameObject [] HeartSteles = new GameObject [8];
    public GameObject [] Torche = new GameObject [8];
    public int [] IndexHeartSteles = new int [8];
    bool stelesGreenOn, stelesYellowOn;
    public GameObject thunder, forceKossi, limitBoss, murBoss, fakeForceKossi;
    public bool inSecteurFour;

    public GameObject kaoBoss, explosionFx, runeAltar;

    void Start()
    {
        bomboktanManager = FindObjectOfType<BomboktanManager>();
        inSecteurFour = false;
        stelesGreenOn = stelesYellowOn = false;
        thunder = transform.GetChild(0).gameObject;
        forceKossi = transform.GetChild(1).gameObject;
        kaoBoss.SetActive(false);
        FindObjectOfType<PlayerUIManager>().transform.GetChild(1).gameObject.SetActive(false);

        if(playerManager.canThunder && playerManager.canBaemb)
        {
                for(int k = 0; k < 8; k++) Torche[k].transform.parent.GetChild(1).gameObject.SetActive(true);
                storyManager.storyStep = 6;
                limitBoss.SetActive(false);
                murBoss.SetActive(false);
                fakeForceKossi.SetActive(false);
                Destroy(this);

        }
        cameraShake = FindObjectOfType<CameraShake>();
        HandleHeartSteleActivationWithIndex();
        HandleBaliseLight();
    }

    void Update()
    {
        HandleBaliseLight();
    }

    void LateUpdate()
    {
        
        if(inSecteurFour)
        {
            if(playerManager.canThunder && playerManager.canBaemb)
            return;

            HandleThunderActivation();
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

    void HandleThunderActivation()
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

            if(stelesGreenOn && !thunder.activeSelf)
            {
                GetComponent<Renderer>().material = altarGreenMat;
                thunder.SetActive(true);
                if(fakeForceKossi != null) fakeForceKossi.SetActive(false);
                forceKossi.SetActive(false);
                GetComponent<AudioSource>().enabled = true;
            }
            else if(stelesYellowOn)
            {
                GetComponent<Renderer>().material = altarYellowmat;
                if(!playerManager.canBaemb && fakeForceKossi != null)
                {
                    fakeForceKossi.SetActive(true);
                    Save();
                }
            }
            else if(!stelesGreenOn)
            {
                thunder.SetActive(false);
                if(fakeForceKossi != null) fakeForceKossi.SetActive(false);
                GetComponent<Renderer>().material = altarOriginMat;
                GetComponent<AudioSource>().enabled = false;
            } else if(stelesGreenOn && playerManager.canThunder && fakeForceKossi != null) fakeForceKossi.SetActive(false);
        }

    }

    void HandleForceKossiActivation()
    {
        bool endLoop = true;

        if(stelesYellowOn == false)
        {
            foreach (int indexHeart in IndexHeartSteles)
            {
                if(indexHeart != 2)
                {
                    endLoop = false;
                    break;
                }
            }
        }

        if(endLoop) 
        {
            stelesYellowOn = true;
            Torche.All(t => !t.activeSelf);
        }
 
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

        if(playerManager.canThunder) 
        {
            thunder.SetActive(false);
            GetComponent<AudioSource>().Stop();
        }
        if(playerManager.canBaemb) kaoBoss.SetActive(false);


    }

    ///////////////////////////////////////////////        FIN A RETIRER    ////////////////////////////////////////////////////////////////////////////

    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3 && stelesGreenOn)
        {
            if(playerManager.canThunder ) return;
            inSecteurFour = true;
            StartCoroutine(arcLightEvent());
        }
        else if(other.gameObject.layer == 3 && stelesYellowOn)
        {
            if(playerManager.canBaemb) return;

            else if(!playerManager.canBaemb && !kaoBoss.activeSelf)StartCoroutine(KaoBossEvent());
            
            else if(kaoBoss.activeSelf)
            {
                if(fakeForceKossi == null) StartCoroutine(ForceKossiEvent());
            }

            inSecteurFour = true;

        }
    }

    IEnumerator arcLightEvent()
    {
        animatorManager.PlayTargetAnimation("PowerUp", true);
        StartCoroutine(gameManager.StartHandleAchievement("--Initiation au Nson--"));
        playerManager.canThunder = true;

        thunder.GetComponent<ParticleSystem>().Stop();

        thunder.SetActive(false);
        yield return new WaitForSeconds(3f);
        GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        storyManager.storyStep = 4;
        bomboktanManager.Spawn(3);
        Invoke("Save", 5f);
        
    }

    IEnumerator ForceKossiEvent()
    {
        animatorManager.PlayTargetAnimation("PowerUp Baemb", true);
        StartCoroutine(gameManager.StartHandleAchievement("--L'esprit de la pierre--"));
        playerManager.canBaemb = true;    
        forceKossi.SetActive(false);

        yield return new WaitForSeconds(6f);
        GetComponent<AudioSource>().enabled = false;
        storyManager.storyStep = 6;
        murBoss.SetActive(false);
        //Pensez à faire spawn Bomboktan
        Invoke("Save", 5f);   
    }

    IEnumerator KaoBossEvent()
    {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.ClearLockOnTargets();
        kaoBoss.SetActive(true);
        buffaloManager = kaoBoss.GetComponent<BuffaloManager>();
        yield return new WaitForSeconds(1f);

        cameraManager.availableTargets.Add(kaoBoss.GetComponent<BuffaloManager>());
        cameraManager.nearestLockOnTarget = cameraManager.availableTargets[0];
        cameraManager.currentLockOnTarget = cameraManager.nearestLockOnTarget;
        FindObjectOfType<InputManager>().lockOnFlag = true;
        limitBoss.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        GetComponent<AudioSource>().enabled = false;
        cameraShake.Shake(6, 0.5f);
        yield return new WaitForSeconds(1f);
        bossThemeAudioSource.Play();
        murBoss.SetActive(true);

        yield return new WaitForSeconds(5.5f);
        kaoBoss.GetComponent<BossHealthBar>().bossHUD.SetActive(true);
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

    public void HandleActivationCraneEvent()
    {

            /*storyManager.storyStep = 6;
            foreach(GameObject steles in HeartSteles)
            {
                steles.GetComponent<Renderer>().material = altarOriginMat;
            }*/

            StartCoroutine(HandleBigKossiFlame());
        
        
            IEnumerator HandleBigKossiFlame()
            {
                for(int k = 0; k < 8; k++)
                {
                    Torche[k].transform.parent.GetChild(1).gameObject.SetActive(true);
                    yield return new WaitForSeconds(2.5f);
                }
                explosionFx.SetActive(true);
                Destroy(fakeForceKossi);
                //FindObjectOfType<BomboktanManager>().DisappearBomboktanSkinnedMeshRenderer();
                
                //this.GetComponent<MeshRenderer>().enabled = false;
                yield return new WaitForSeconds (0.05f);
                forceKossi.SetActive(true);
                //yield return new WaitForSeconds (4f);
                //Destroy(this.gameObject);
                
            }
        }
}
