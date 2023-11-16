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
        if(dayNightCycleManager.dayTimer > 1080 || dayNightCycleManager.dayTimer <= 300)
        {
            RenderSettings.skybox = dayPeriodSkyboxes[2];
            foreach(var elt in fireLights) elt.Play();
            foreach (var elt in pointLights) elt.enabled = true;
        }
        else if(dayNightCycleManager.dayTimer > 300 || dayNightCycleManager.dayTimer <= 1080)
        {
            foreach (var elt in fireLights) elt.Stop();
            foreach (var elt in pointLights) elt.enabled = false;
            
            if(dayNightCycleManager.dayTimer < 720) RenderSettings.skybox = dayPeriodSkyboxes[0];
            else RenderSettings.skybox = dayPeriodSkyboxes[1];
        }
              
    }

}
