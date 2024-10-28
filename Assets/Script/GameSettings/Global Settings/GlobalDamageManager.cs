using UnityEngine;
using SJ;

[DefaultExecutionOrder(+2)]
public class GlobalDamageManager : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField] int damage;

    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
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
