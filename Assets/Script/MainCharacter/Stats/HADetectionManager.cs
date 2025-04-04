using UnityEngine;

namespace SJ
{
    public class HADetectionManager : MonoBehaviour
    {
        PlayerAttacker playerAttacker;
        [SerializeField] GameObject wishShoke;


        void Awake()
        {
            playerAttacker = FindFirstObjectByType<PlayerAttacker>();
        }
        // Start is called before the first frame update
        void OnTriggerEnter(Collider other)
        {
            Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0, 1f, 0f);
            if(other.gameObject.layer == 10)
            {
                
                Instantiate (wishShoke, impactPosition, Quaternion.identity);                
                VaseContainerManager vaseContainerManager = other.GetComponent<VaseContainerManager>();

                if(vaseContainerManager != null) vaseContainerManager.HandleVaseConatinerProcess();
            }

            if(other.gameObject.layer == 13)
            {
                if(other.gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem component))
                {
                    Instantiate (wishShoke, impactPosition, Quaternion.identity);
                    Destroy(component.gameObject);
                }
            }

            if(other.gameObject.layer == 12)
            {
                Instantiate (wishShoke, impactPosition, Quaternion.identity); 
                if(other.TryGetComponent<EnemyManager>(out EnemyManager component))
                {
                    if(component is TololManager tololManager)
                    {
                        tololManager.TakeDamage(playerAttacker.statesJiataData.d_HighAttack);
                        playerAttacker.isHit = true;
                    }
                    if(component is KossiManager kossiManager)
                    {
                        kossiManager.TakeDamage(playerAttacker.statesJiataData.d_HighAttack);
                    }
                    else if(component is kossiKazeManager kossiKazeManager)
                    {
                        kossiKazeManager.kossiKazePattern.HandleExplosion();
                    }
                    else if(component is KeliperManager keliperManager)
                    {
                        keliperManager.keliperPattern.keliperAnimatorManager.anim.SetBool("isHit", true);
                        keliperManager.TakeDamage(playerAttacker.statesJiataData.d_HighAttack);
                        if(keliperManager.keliperPattern.currentTarget == null) 
                        {
                            keliperManager.keliperPattern.currentTarget = FindFirstObjectByType<PlayerManager>();
                            keliperManager.isPreformingAction = false;
                        }
                    }
                    else if(component is BuffaloManager buffaloManager)
                        {
                            if(buffaloManager.isArmor) return;

                            float distance = buffaloManager.buffaloPattern.distanceFromTarget;
                            buffaloManager.TakeDamage(playerAttacker.statesJiataData.d_HighAttack * 2); 
                        }

                }

                //jouer l'animation du choc
            }
        }

    }
}

