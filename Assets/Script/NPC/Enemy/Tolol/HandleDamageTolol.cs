using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class HandleDamageTolol : MonoBehaviour
{

    PlayerStats playerStats;
    PlayerManager playerManager;
    PlayerAttacker playerAttacker;
    [SerializeField] GameObject bulletImpact;
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
            Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0f, 1f, 0f);
            Instantiate(bulletImpact, impactPosition, Quaternion.identity);
            playerStats.TakeDamage(damage, 0);
            playerManager.takeDamage = true;
            
        }
    }
}
