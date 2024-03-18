using UnityEngine;
using UnityEngine.UI;

public class SibongoManager : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;
    SibongoManager sibongoManager;
    public ParticleSystem [] fireLights;
    //public Light [] pointLights;
    public Material[] dayPeriodSkyboxes;
    public int dayPeriod;
    public GameObject fireFly, sun, tBilol, tLibum, tNgoMaa, tNgonda;
    [SerializeField] GameObject lightPortal;
    AudioSource sibongoAudiosource;
    public AudioClip [] sibongoAmbiances = new AudioClip[3];


    void OnEnable()
    {
        dayNightCycleManager = FindObjectOfType<DayNightCycleManager>();
        sibongoManager = FindObjectOfType<SibongoManager>();
        sibongoAudiosource = GetComponent<AudioSource>();
        dayPeriod = dayNightCycleManager.dayTimer; 
    }
    
    void Start()
    {
        if(sibongoManager.dayPeriod > 2)
        {
            RenderSettings.skybox = dayPeriodSkyboxes[2];
            foreach(var elt in fireLights) elt.Play();
            fireFly.SetActive(true);
            sun.SetActive(false);
            lightPortal.SetActive(true);
        }
        else
        {
            foreach (var elt in fireLights) 
            {
                elt.Stop();
            }
            fireFly.SetActive(false);
            lightPortal.SetActive(false);
            
            if(sibongoManager.dayPeriod == 0) 
            {
                RenderSettings.skybox = dayPeriodSkyboxes[0];
                float _xAxis = Random.Range(0,30);
                Quaternion rotation = Quaternion.Euler(_xAxis, 0f, 0f);
                sun.transform.rotation = rotation;  
            }
            else if(sibongoManager.dayPeriod == 1)
            {
                float _xAxis = Random.Range(30,110);
                Quaternion rotation = Quaternion.Euler(_xAxis, 0f, 0f);
                sun.transform.rotation = rotation; 
                RenderSettings.skybox = dayPeriodSkyboxes[1];
            }
            else if(sibongoManager.dayPeriod == 2)
            {
                float _xAxis = Random.Range(110,160);
                Quaternion rotation = Quaternion.Euler(_xAxis, 0f, 0f);
                sun.transform.rotation = rotation; 
                RenderSettings.skybox = dayPeriodSkyboxes[1];
            }
        }
        HandleAmbiance();         
    }

    void HandleAmbiance()
    {
        if(sibongoManager.dayPeriod <= 1) sibongoAudiosource.clip = sibongoAmbiances[0];
        else if(sibongoManager.dayPeriod == 2) sibongoAudiosource.clip = sibongoAmbiances[1];
        else if(sibongoManager.dayPeriod >= 3) 
        {
            tBilol.SetActive(true);
            tLibum.SetActive(true);
            tNgonda.SetActive(true);
            tNgoMaa.SetActive(true);
            sibongoAudiosource.clip = sibongoAmbiances[2];
        }

        sibongoAudiosource.Play();
    }

}
