using SJ;
using UnityEngine;

public class KikohaManager : MonoBehaviour
{
    [SerializeField] GameObject kikohaImpact;
    int kikohaDamage = 5;

    void OnTriggerEnter(Collider other)
    {
        Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0, 1f, 0f);

        if(other.gameObject.layer == 12)
        {     
            if(other.gameObject.TryGetComponent<EnemyManager>(out EnemyManager component))
            {
                Instantiate (kikohaImpact, impactPosition, Quaternion.identity);
                
                PlayerAttacker playerAttacker = FindObjectOfType<PlayerAttacker>();
                if(component is TololManager tololManager)
                {
                    tololManager.TakeDamage(kikohaDamage);
                    playerAttacker.isHit = true;
                    //component.gameObject.GetComponent<TololAnimatorManager>().anim.SetBool("isACHit", true);
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
            if(other.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager component))
            {
                component.HandleVaseConatinerProcess();
            }
        }
    }
}
