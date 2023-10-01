using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class HandleDamageTolol : MonoBehaviour
{

    PlayerStats playerStats;
    PlayerManager playerManager;
    PlayerAttacker playerAttacker;
    int damage;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerManager = FindObjectOfType<PlayerManager>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
    }


    public void FixDamage(int tempdamage)
    {
        damage = tempdamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            playerStats.TakeDamage(damage);
            playerManager.takeDamage = true;
        }
    }
}
