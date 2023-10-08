using UnityEngine;
using SJ;
using System.Drawing;

public class BulleManager : MonoBehaviour
{

    PlayerStats playerStats;
    public GameObject bulletImpact;
    [SerializeField] int bulletDamage = 5;


    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();    
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3)
        {
            playerStats.TakeDamage(bulletDamage, 0);
            Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0f, 1.7f, 0f);
            Instantiate(bulletImpact, impactPosition, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);
        }
        else if(other.gameObject.layer == 10)
        {
            if(other.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager component))
            {
                component.HandleVaseConatinerProcess();
            }
        }

        
    }
}
