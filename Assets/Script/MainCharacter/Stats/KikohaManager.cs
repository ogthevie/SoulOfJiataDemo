using SJ;
using UnityEngine;

public class KikohaManager : MonoBehaviour
{
    [SerializeField] GameObject kikohaImpact, wishShoke;
    [SerializeField] PlayerAttacker playerAttacker;
    [SerializeField] AudioClip kikohaImpactSFX;
    [SerializeField] AudioSource kikohaAudioSource;
    int kikohaDamage = 5;

    private void Awake()
    {
        playerAttacker = FindFirstObjectByType<PlayerAttacker>();  
        kikohaAudioSource = FindFirstObjectByType<PlayerUIManager>().GetComponent<AudioSource>();  
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject impact = Instantiate(kikohaImpact, transform.position, Quaternion.identity, null);
        kikohaAudioSource.PlayOneShot(kikohaImpactSFX);
        
        if(other.gameObject.layer == 8)
        {
            Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0, 1f, 0f);
            other.gameObject.GetComponent<Rigidbody>().AddForce(playerAttacker.transform.forward * 50f, ForceMode.Impulse);
        }
        else if(other.gameObject.layer == 12)
        {

            if(other.gameObject.TryGetComponent<EnemyManager>(out EnemyManager component))
            {
                PlayerAttacker playerAttacker = FindFirstObjectByType<PlayerAttacker>();
                if(component is TololManager tololManager)
                {
                    if(component.isbreak) component.TakeDamage(130);
                    else component.TakeDamage(kikohaDamage);

                    if(component == null) return;

                    playerAttacker.isHit = true;
                    if(tololManager.tololPattern.currentTarget == null) 
                    {
                        tololManager.tololPattern.currentTarget = FindFirstObjectByType<PlayerManager>();
                        tololManager.isPreformingAction = false;
                    }                      
                }
                else if(component is KossiManager kossiManager)
                {
                    if(component.isbreak) component.TakeDamage(130);
                    else component.TakeDamage(kikohaDamage);

                    if(component == null) return;
                    

                    if(kossiManager.kossiPattern.currentTarget == null) 
                    {
                        kossiManager.kossiPattern.currentTarget = FindFirstObjectByType<PlayerManager>();
                        kossiManager.isPreformingAction = false;
                    }
                }
                else if(component is KeliperManager keliperManager)
                {
                    if(component.isbreak) component.TakeDamage(130);
                    else component.TakeDamage(kikohaDamage);

                    if(component == null) return;

                    keliperManager.keliperPattern.keliperAnimatorManager.anim.SetBool("isHit", true);
                    if(keliperManager.keliperPattern.currentTarget == null) 
                    {
                        keliperManager.keliperPattern.currentTarget = FindFirstObjectByType<PlayerManager>();
                        keliperManager.isPreformingAction = false;
                    }
                }
                else if(component is kossiKazeManager kossiKazeManager)
                {
                    kossiKazeManager.kossiKazePattern.HandleExplosion();
                    if(kossiKazeManager.kossiKazePattern.currentTarget == null) 
                    {
                        kossiKazeManager.kossiKazePattern.currentTarget = FindFirstObjectByType<PlayerManager>();
                        kossiKazeManager.isPreformingAction = false;
                    }                             
                }
                else if(component is BuffaloManager buffaloManager)
                {
                    if(buffaloManager.isArmor || !buffaloManager.isReady) return;
                    
                    buffaloManager.TakeDamage(kikohaDamage);
                    if(buffaloManager.buffaloPattern.currentTarget == null) 
                    {
                        buffaloManager.buffaloPattern.currentTarget = FindFirstObjectByType<PlayerManager>();
                    }                             
                }
                //Destroy(gameObject);
            }
        }
        else if(other.gameObject.layer == 10)
        {
            if(other.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager component))component.HandleVaseConatinerProcess();
        }

        Destroy(this.gameObject, 0.1f);
    }
}
