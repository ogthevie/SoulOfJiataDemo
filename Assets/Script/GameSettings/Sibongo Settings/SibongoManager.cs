using UnityEngine;

[DefaultExecutionOrder(-1)]

public class SibongoManager : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;
    public ParticleSystem [] fireLights;
    //public Light [] pointLights;
    public Material[] dayPeriodSkyboxes;
    [SerializeField] Material activatematerial;
    public int dayPeriod;
    public GameObject sun, tBilol, tLibum, tNgoMaa, tNgonda, fireKokoa;
    [SerializeField] GameObject lightPortal;
    [SerializeField] GameObject house05, house09, house07, house04, house03, nyooCrane, sibongoGate;
    AudioSource sibongoAudiosource;
    [SerializeField] AudioSource umAudioSource;
    public AudioClip [] sibongoAmbiances = new AudioClip[3];


    void OnEnable()
    {
        dayNightCycleManager = FindFirstObjectByType<DayNightCycleManager>();

        sibongoAudiosource = GetComponent<AudioSource>();
        HandleDayPeriod();
        HandleLightConstruction();
    }

    public void HandleDayPeriod()
    {
        dayPeriod = dayNightCycleManager.dayTimer;
        
        StoryManager storyManager;
        storyManager = FindFirstObjectByType<StoryManager>();

        if(dayPeriod > 2)
        {
            RenderSettings.skybox = dayPeriodSkyboxes[2];
            foreach(var elt in fireLights) elt.Play();
            sun.SetActive(false);
            lightPortal.SetActive(true);
            umAudioSource.enabled = false;
            fireKokoa.SetActive(true);
        }
        else
        {
            fireKokoa.SetActive(false);
            foreach (var elt in fireLights) 
            {
                elt.Stop();
            }
            lightPortal.SetActive(false);
            
            if(!umAudioSource.enabled)
            {
                umAudioSource.enabled = true;
                umAudioSource.Play();
            }

            if(dayPeriod == 0) 
            {
                RenderSettings.skybox = dayPeriodSkyboxes[0];
                Quaternion rotation = Quaternion.Euler(15f, 0f, 0f);
                sun.transform.rotation = rotation;  
            }
            else if(dayPeriod == 1)
            {
                Quaternion rotation = Quaternion.Euler(70f, 0f, 0f);
                sun.transform.rotation = rotation; 
                RenderSettings.skybox = dayPeriodSkyboxes[1];
            }
            else if(dayPeriod == 2)
            {
                Quaternion rotation = Quaternion.Euler(125, 0f, 0f);
                sun.transform.rotation = rotation; 
                RenderSettings.skybox = dayPeriodSkyboxes[1];
            }
        }
        
        if(dayPeriod < 3)
        {
            tBilol.SetActive(false);
            tLibum.SetActive(false);
            tNgonda.SetActive(false);
            tNgoMaa.SetActive(false);
            if(dayPeriod <= 1) sibongoAudiosource.clip = sibongoAmbiances[0];     
            else if(dayPeriod == 2) sibongoAudiosource.clip = sibongoAmbiances[1];       
        }
        else if(dayPeriod >= 3) 
        {
            tBilol.SetActive(true);
            tLibum.SetActive(true);
            tNgonda.SetActive(true);
            tNgoMaa.SetActive(true);
            sibongoAudiosource.clip = sibongoAmbiances[2];
        }

        sibongoAudiosource.Play();    
    }

    public void HandleLightConstruction()
    {
        StoryManager storyManager = dayNightCycleManager.GetComponent<StoryManager>();

        if(storyManager.storyStep >= 6)
        {
            house05.transform.GetChild(0).gameObject.SetActive(true);

            var hNineMat = house09.GetComponent<Renderer>().materials;
            var hSevenMat = house07.GetComponent<Renderer>().materials;
            var hFourMat = house04.GetComponent<Renderer>().materials;
            var hThreeMat = house03.GetComponent<Renderer>().materials;
            var nyooMat = nyooCrane.GetComponent<Renderer>().materials;
            var sibGateMat = sibongoGate.GetComponent<Renderer>().materials;

            hNineMat[3] = hSevenMat[2] =  hFourMat[1] = hThreeMat[1] = nyooMat[1] = sibGateMat[2] = activatematerial;


            house09.GetComponent<Renderer>().materials = hNineMat;
            house07.GetComponent<Renderer>().materials = hSevenMat;
            house04.GetComponent<Renderer>().materials = hFourMat;
            house03.GetComponent<Renderer>().materials = hThreeMat;
            nyooCrane.GetComponent<Renderer>().materials = nyooMat;
            sibongoGate.GetComponent<Renderer>().materials = sibGateMat;
        }


    }
}
