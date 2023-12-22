using UnityEngine;
using UnityEngine.UI;

public class SibongoManager : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;
    SibongoManager sibongoManager;
    public ParticleSystem [] fireLights;
    public Light [] pointLights;
    public Material[] dayPeriodSkyboxes;
    public GameObject[] HommPosition; // les activites des PNJ se divisent en 05 périodes de la journée de la journéee
    public int dayPeriod;
    public GameObject fireFly;
    GameObject dayPeriodPanel;
    Image [] dayPeriodIcon = new Image[5];


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


    void OnEnable()
    {
        dayNightCycleManager = FindObjectOfType<DayNightCycleManager>();
        sibongoManager = FindObjectOfType<SibongoManager>();
        dayNightCycleManager.dayTimer += 180f;
        TimerRoutine();  
    }

    void Start()
    {

        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        sun.transform.rotation = Quaternion.identity;

        if(sibongoManager.dayPeriod == 3 || sibongoManager.dayPeriod == 4)
        {
            RenderSettings.skybox = dayPeriodSkyboxes[2];
            foreach(var elt in fireLights) elt.Play();
            foreach (var elt in pointLights) elt.enabled = true;
            fireFly.SetActive(true);
            Quaternion rotation = Quaternion.Euler(-40f, 0f, 0f);
            sun.transform.rotation = rotation; 
        }
        else
        {
            foreach (var elt in fireLights) elt.Stop();
            foreach (var elt in pointLights) elt.enabled = false;
            fireFly.SetActive(false);
            
            if(sibongoManager.dayPeriod == 0) 
            {
                RenderSettings.skybox = dayPeriodSkyboxes[0];
                Quaternion rotation = Quaternion.Euler(15f, 0f, 0f);
                sun.transform.rotation = rotation;  
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(45f, 0f, 0f);
                sun.transform.rotation = rotation; 
                RenderSettings.skybox = dayPeriodSkyboxes[1];
            }
        }

        foreach(var elt in dayPeriodIcon) elt.enabled = false;

        dayPeriodIcon[dayPeriod].enabled = true;         
    }

    void TimerRoutine()
    {
        if(dayNightCycleManager.dayTimer >= 360f && dayNightCycleManager.dayTimer < 600f) dayPeriod = 0;
        else if(dayNightCycleManager.dayTimer >= 600f && dayNightCycleManager.dayTimer < 960f) dayPeriod = 1;
        else if(dayNightCycleManager.dayTimer >= 960f && dayNightCycleManager.dayTimer < 1140f) dayPeriod = 2;
        else if(dayNightCycleManager.dayTimer >= 1140f && dayNightCycleManager.dayTimer < 1320f) dayPeriod = 3;
        else if(dayNightCycleManager.dayTimer >= 1320f || dayNightCycleManager.dayTimer < 360f) dayPeriod = 4;
    }

}
