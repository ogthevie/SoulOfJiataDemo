using SJ;
using UnityEngine;

public class MagnetSphereManager : MonoBehaviour
{
    Rigidbody msRigibody;
    AudioSource audioSource;
    PlayerStats playerStats;
    PlayerAttacker playerAttacker;
    Material magnetSphereMaterial;
    readonly int flameDamage = 6;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        msRigibody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        magnetSphereMaterial = GetComponent<Renderer>().materials[1];
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
        else if(other.collider.gameObject.layer == 12)
        {
            if(other.collider.TryGetComponent<EnemyManager>(out EnemyManager component))
            {

                if(component is kossiKazeManager kossiKazeManager)
                {
                    kossiKazeManager.kossiKazePattern.HandleExplosion();
                    Destroy(this.gameObject);
                }
                else if(component is KossiManager kossiManager)
                {
                    kossiManager.TakeDamage(flameDamage);
                }
            }
        }
        else if(other.gameObject.tag == "Stele")
        {
            if(this.transform.GetChild(0).gameObject.activeSelf)
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = playerAttacker.lightingMaterials[0];
            else if(this.transform.GetChild(1).gameObject.activeSelf)
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = playerAttacker.lightingMaterials[1];
        }
    }

    void LateUpdate()
    {
        if(!audioSource.enabled)
        {
            if(this.transform.GetChild(0).gameObject.activeSelf || this.transform.GetChild(1).gameObject.activeSelf)
            audioSource.enabled = true;        
        }
    }
}
