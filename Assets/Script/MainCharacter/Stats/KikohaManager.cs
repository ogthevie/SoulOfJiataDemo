using SJ;
using UnityEngine;

public class KikohaManager : MonoBehaviour
{
    [SerializeField] GameObject kikohaImpact, wishShoke;
    [SerializeField] PlayerAttacker playerAttacker;
    int kikohaDamage = 5;

    private void Awake()
    {
        playerAttacker = FindObjectOfType<PlayerAttacker>();    
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0, 1f, 0f);

        if(other.gameObject.layer == 13)
        {
            if(other.gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem component))
            {
                Instantiate (wishShoke, impactPosition, Quaternion.identity);
                Destroy(component.gameObject);
                Destroy(this.gameObject);
            }
        }
        if(other.gameObject.layer == 8)
        {
            Instantiate(kikohaImpact, impactPosition, Quaternion.identity);
            if(!other.transform.GetChild(1).gameObject.activeSelf) 
            {
                playerAttacker.audioManager.ReadMagnetiFireSphereFx();
                other.transform.GetChild(1).gameObject.SetActive(true);
                other.GetComponent<MagnetSphereManager>().enabled = true;
            }
        }
        if(other.gameObject.layer == 12)
        {
            Instantiate (kikohaImpact, impactPosition, Quaternion.identity);

            if(other.gameObject.TryGetComponent<EnemyManager>(out EnemyManager component))
            {
                PlayerAttacker playerAttacker = FindObjectOfType<PlayerAttacker>();
                if(component is TololManager tololManager)
                {
                    tololManager.TakeDamage(kikohaDamage);
                    playerAttacker.isHit = true;
                    if(tololManager.tololPattern.currentTarget == null) 
                    {
                        tololManager.tololPattern.currentTarget = FindObjectOfType<PlayerManager>();
                        tololManager.isPreformingAction = false;
                    }                      
                }
                else if(component is KossiManager kossiManager)
                {
                    kossiManager.TakeDamage(kikohaDamage);
                    if(kossiManager.kossiPattern.currentTarget == null) 
                    {
                        kossiManager.kossiPattern.currentTarget = FindObjectOfType<PlayerManager>();
                        kossiManager.isPreformingAction = false;
                    }
                }
                else if(component is KeliperManager keliperManager)
                {
                    keliperManager.TakeDamage(kikohaDamage);
                    keliperManager.keliperPattern.keliperAnimatorManager.anim.SetBool("isHit", true);
                    if(keliperManager.keliperPattern.currentTarget == null) 
                    {
                        keliperManager.keliperPattern.currentTarget = FindObjectOfType<PlayerManager>();
                        keliperManager.isPreformingAction = false;
                    }
                }
                else if(component is kossiKazeManager kossiKazeManager)
                {
                    kossiKazeManager.kossiKazePattern.HandleExplosion();
                    if(kossiKazeManager.kossiKazePattern.currentTarget == null) 
                    {
                        kossiKazeManager.kossiKazePattern.currentTarget = FindObjectOfType<PlayerManager>();
                        kossiKazeManager.isPreformingAction = false;
                    }                             
                }
                else if(component is BuffaloManager buffaloManager)
                {
                    if(buffaloManager.isArmor) return;
                    
                    buffaloManager.TakeDamage(kikohaDamage);
                    if(buffaloManager.buffaloPattern.currentTarget == null) 
                    {
                        buffaloManager.buffaloPattern.currentTarget = FindObjectOfType<PlayerManager>();
                    }                             
                }
                //Destroy(gameObject);
            }
        }
        else if(other.gameObject.layer == 10)
        {
            if(other.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager component))component.HandleVaseConatinerProcess();
        }
    }
}
