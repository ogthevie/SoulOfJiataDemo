using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    Light sun;
    public double degreeSun;
    float speed = 3f;
    public float dayTimer;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sun"); // Recherche d'objets avec le tag spécifique

        if (objs.Length > 1) // Vérifier s'il y a déjà des objets persistants présents
        {
            // Si oui, détruire les doublons
            for (int i = 1; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }
        }
        DontDestroyOnLoad(this.gameObject);
        
        sun = GetComponent<Light>();
        
    }

    void Update()
    {
        float delta = Time.deltaTime;
        dayTimer += delta;
        HandleCycle(delta);
    }

    void HandleCycle(float delta)
    {
        sun.transform.Rotate(Vector3.right * speed * Time.deltaTime);
        degreeSun = transform.localEulerAngles.x;

        if(degreeSun > 180) degreeSun -= 360;
        sun.intensity = (float)(0.0131962 * degreeSun + 1.16568); 

    }

}
