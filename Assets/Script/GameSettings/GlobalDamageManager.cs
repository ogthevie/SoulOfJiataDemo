using UnityEngine;
using SJ;

public class GlobalDamageManager : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField] int damage;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        //this.GetComponent<Collider>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            playerStats.TakeDamage(damage, 1);
            //this.GetComponent<Collider>().enabled = false;
        }
    }
}
