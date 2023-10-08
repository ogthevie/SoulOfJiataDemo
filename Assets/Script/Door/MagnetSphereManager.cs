using SJ;
using UnityEngine;

public class MagnetSphereManager : MonoBehaviour
{
    Rigidbody msRigibody;
    VaseContainerManager vaseContainerManager;
    PlayerStats playerStats;
    readonly int flameDamage = 6;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        msRigibody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        msRigibody.isKinematic = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3)
        {
            playerStats.TakeDamage(flameDamage, 1);
        }
        else if(other.gameObject.layer == 10)
        {
            if(other.collider.TryGetComponent<VaseContainerManager>(out VaseContainerManager component)) component.HandleVaseConatinerProcess();
        }
    }
}
