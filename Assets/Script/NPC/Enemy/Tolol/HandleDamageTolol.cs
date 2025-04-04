using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class HandleDamageTolol : MonoBehaviour
{

    PlayerStats playerStats;
    PlayerManager playerManager;
    [SerializeField] GameObject bulletImpact;
    int damage;

    void Awake()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        playerManager = FindFirstObjectByType<PlayerManager>();
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
        else if(other.gameObject.layer == 10) if(other.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager component))component.HandleVaseConatinerProcess();
    }
}
