using UnityEngine;

public class SibongoManager : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;
    public ParticleSystem [] fireLights;
    public Light [] pointLights;
    public Material[] dayPeriodSkyboxes;

    void Awake()
    {
        dayNightCycleManager = FindObjectOfType<DayNightCycleManager>();
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

}
