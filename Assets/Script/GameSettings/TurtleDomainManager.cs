using UnityEngine;
using SJ;

public class TurtleDomainManager : MonoBehaviour
{
    PlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) 
        {
             
            playerStats.GetComponent<AudioManager>().TurtleDomain();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            playerStats.TakeStaminaDamage(50);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3) 
        {
            playerStats.GetComponent<AudioManager>().TurtleDomain();
        }
    }
}
