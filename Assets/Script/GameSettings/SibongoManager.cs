using UnityEngine;
using UnityEngine.AI;

public class SibongoManager : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;
    public ParticleSystem [] fireLights;
    public Light [] pointLights;
    public Material[] dayPeriodSkyboxes;
    public GameObject[] HommPosition; // les activites des PNJ se divisent en 05 périodes de la journée de la journéee
    public int dayPeriod;


    void Awake()
    {
        dayNightCycleManager = FindObjectOfType<DayNightCycleManager>();
        dayNightCycleManager.dayTimer += 180f;
        TimerRoutine();  
    }

    void Start()
    {

        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        sun.transform.rotation = Quaternion.identity;

        if(dayNightCycleManager.dayTimer > 1080 || dayNightCycleManager.dayTimer <= 300)
        {
            RenderSettings.skybox = dayPeriodSkyboxes[2];
            foreach(var elt in fireLights) elt.Play();
            foreach (var elt in pointLights) elt.enabled = true;
            Quaternion rotation = Quaternion.Euler(-40f, 0f, 0f);
            sun.transform.rotation = rotation; 
        }
        else if(dayNightCycleManager.dayTimer > 300 || dayNightCycleManager.dayTimer <= 1080)
        {
            foreach (var elt in fireLights) elt.Stop();
            foreach (var elt in pointLights) elt.enabled = false;
            
            if(dayNightCycleManager.dayTimer < 720) 
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
