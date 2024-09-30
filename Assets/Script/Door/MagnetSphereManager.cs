using System.Collections;
using SJ;
using UnityEngine;

public class MagnetSphereManager : MonoBehaviour
{
    Rigidbody msRigibody;
    AudioSource audioSource;
    PlayerStats playerStats;
    PlayerAttacker playerAttacker;
    MagnetoSourceManager magnetoSourceManager;
    Material magnetSphereMaterial;
    [SerializeField] readonly int flameDamage = 6;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerAttacker = playerStats.GetComponent<PlayerAttacker>();
        magnetoSourceManager = FindObjectOfType<MagnetoSourceManager>();
        msRigibody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        magnetSphereMaterial = GetComponent<Renderer>().materials[1];
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


        if(!audioSource.enabled)
        {
            if(other.gameObject.layer == 3)
            {
                msRigibody.isKinematic = true;
            }            
        }
        else
        {
            if(other.gameObject.layer == 3)
            {
                playerStats.TakeDamage(flameDamage, 1);
            }
            else if(other.collider.gameObject.layer == 12)
            {
                if(other.collider.TryGetComponent<EnemyManager>(out EnemyManager component))
                {

                    if(component is kossiKazeManager kossiKazeManager)
                    {
                        kossiKazeManager.kossiKazePattern.HandleExplosion();
                        HandleDestroyMagnetSphere();
                    }
                }
            }
            else if(other.gameObject.tag == "Stele")
            {
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = playerAttacker.lightingMaterials[0];
                other.gameObject.GetComponent<AudioSource>().Play();
            }   
        }

    }

    public void HandleDestroyMagnetSphere()
    {
        magnetoSourceManager.HandleSpawnMagnetSphere();
        Destroy(this.gameObject);
    }


}
