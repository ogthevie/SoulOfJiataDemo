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
    
    AudioSource sibongoAudiosource;
    public AudioClip [] sibongoAmbiances = new AudioClip[3];
    GameObject dayPeriodPanel;
    Image [] dayPeriodIcon = new Image[5];


    void OnEnable()
    {
        dayNightCycleManager = FindObjectOfType<DayNightCycleManager>();
        sibongoManager = FindObjectOfType<SibongoManager>();
        sibongoAudiosource = GetComponent<AudioSource>();
        dayNightCycleManager.dayTimer += 180f;
        TimerRoutine();  
    }

    void Awake()
    {
        dayPeriodPanel = FindObjectOfType<PlayerUIManager>().transform.GetChild(4).gameObject;
        dayPeriodPanel.SetActive(true);
        dayPeriodIcon[0] = dayPeriodPanel.transform.GetChild(0).GetComponent<Image>();
        dayPeriodIcon[1] = dayPeriodPanel.transform.GetChild(1).GetComponent<Image>();
        dayPeriodIcon[2] = dayPeriodPanel.transform.GetChild(2).GetComponent<Image>();
        dayPeriodIcon[3] = dayPeriodPanel.transform.GetChild(3).GetComponent<Image>();
        dayPeriodIcon[4] = dayPeriodPanel.transform.GetChild(4).GetComponent<Image>();
    }
    
    void Start()
    {
        if(sibongoManager.dayPeriod > 2)
        {
            RenderSettings.skybox = dayPeriodSkyboxes[2];
            foreach(var elt in fireLights) elt.Play();
            //foreach (var elt in pointLights) elt.enabled = true;
            fireFly.SetActive(true);
            sun.SetActive(false);
        }
        else
        {
            foreach (var elt in fireLights) elt.Stop();
            //foreach (var elt in pointLights) elt.enabled = false;
            fireFly.SetActive(false);
            
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

        foreach(var elt in dayPeriodIcon) elt.enabled = false;

        dayPeriodIcon[dayPeriod].enabled = true;
        HandleAmbiance();         
    }

    void TimerRoutine()
    {
        if(dayNightCycleManager.dayTimer >= 360f && dayNightCycleManager.dayTimer < 600f) dayPeriod = 0;
        else if(dayNightCycleManager.dayTimer >= 600f && dayNightCycleManager.dayTimer < 960f) dayPeriod = 1;
        else if(dayNightCycleManager.dayTimer >= 960f && dayNightCycleManager.dayTimer < 1140f) dayPeriod = 2;
        else if(dayNightCycleManager.dayTimer >= 1140f && dayNightCycleManager.dayTimer < 1320f) dayPeriod = 3;
        else if(dayNightCycleManager.dayTimer >= 1320f || dayNightCycleManager.dayTimer < 360f) dayPeriod = 4;
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
