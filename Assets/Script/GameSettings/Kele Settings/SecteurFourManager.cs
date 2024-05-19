using UnityEngine;

public class SecteurFourManager : MonoBehaviour
{
    ThunderEventManager thunderEventManager;


    void Awake()
    {
        thunderEventManager = FindObjectOfType<ThunderEventManager>();

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) thunderEventManager.inSecteurFour = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3) thunderEventManager.inSecteurFour = false;
    }
}
