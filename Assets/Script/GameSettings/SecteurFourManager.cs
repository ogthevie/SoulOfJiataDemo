using UnityEngine;

public class SecteurFourManager : MonoBehaviour
{
    ArcLightEventManager arcLightEventManager;


    void Awake()
    {
        arcLightEventManager = FindObjectOfType<ArcLightEventManager>();

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) arcLightEventManager.inSecteurFour = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3) arcLightEventManager.inSecteurFour = false;
    }
}
