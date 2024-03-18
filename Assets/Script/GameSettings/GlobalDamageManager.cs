using UnityEngine;
using SJ;

public class GlobalDamageManager : MonoBehaviour
{
    int dayPeriod;
    PlayerStats playerStats;
    [SerializeField] int damage;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        dayPeriod = FindObjectOfType<SibongoManager>().dayPeriod;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) playerStats.TakeDamage(damage, 1);
    }
}
