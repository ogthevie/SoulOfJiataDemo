using UnityEngine;

public class LightCycleManager : MonoBehaviour
{
    DayNightCycleManager dayNightCycleManager;
    public ParticleSystem [] fireLights;
    public Light [] pointLights;
    public bool lightState;

    void Awake()
    {
        dayNightCycleManager = FindObjectOfType<DayNightCycleManager>();
    }

    void Start()
    {
        foreach (var elt in fireLights)
        {
            elt.Stop();
        }
        foreach (var elt in pointLights)
        {
            elt.enabled = false;
        }
        lightState = false;        
    }

    void LateUpdate()
    {
        HandleLight();
    }

    void HandleLight()
    {
        if(dayNightCycleManager == null)
            return;
        
        if(dayNightCycleManager.degreeSun > 5f && lightState)
        {
            foreach (var elt in fireLights)
            {
                elt.Stop();
            }
            foreach (var elt in pointLights)
            {
                elt.enabled = false;
            }
            lightState = false;
        }

        else if(dayNightCycleManager.degreeSun < 5f && !lightState)
        {
            foreach(var elt in fireLights)
            {
                elt.Play();
            }
            foreach (var elt in pointLights)
            {
                elt.enabled = true;
            }
            lightState = true;
        }
     
    }
}
