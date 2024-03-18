using UnityEngine;
using SJ;

public class SaintFireManager : MonoBehaviour
{
    PlayerStats playerStats;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void OnTriggerEnter(Collider other)
    {
        playerStats.AddHealth(1000);
    }
}
