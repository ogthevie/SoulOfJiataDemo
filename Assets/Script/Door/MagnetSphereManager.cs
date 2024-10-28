using System.Collections;
using SJ;
using UnityEngine;

public class MagnetSphereManager : MonoBehaviour
{
    Rigidbody msRigibody;
    AudioSource audioSource;
    PlayerStats playerStats;
    MagnetoSourceManager magnetoSourceManager;
    [SerializeField] readonly int flameDamage = 6;

    void Awake()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        magnetoSourceManager = FindFirstObjectByType<MagnetoSourceManager>();
        msRigibody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        msRigibody.isKinematic = false;
    }

    void OnEnable()
    {
        if(!audioSource.enabled)
        {
            if(this.transform.GetChild(0).gameObject.activeSelf)
                {
                    audioSource.enabled = true;  
                    msRigibody.isKinematic = false;
                }      
        }
    }
    void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.layer == 3)
        {
            playerStats.TakeDamage(flameDamage, 1);
        }
        else if(other.collider.gameObject.layer == 12)
        {
            if(other.collider.TryGetComponent<EnemyManager>(out EnemyManager component))
            {
                if(component is BuffaloManager buffaloManager) return;

                if(component is kossiKazeManager kossiKazeManager)
                {
                    kossiKazeManager.kossiKazePattern.HandleExplosion();
                    HandleDestroyMagnetSphere();
                }
                else if(msRigibody.linearVelocity.magnitude > 10f) component.TakeDamage(150);
            }
        }

    }

    public void HandleDestroyMagnetSphere()
    {
        magnetoSourceManager.HandleSpawnMagnetSphere();
        Destroy(this.gameObject);
    }


}
