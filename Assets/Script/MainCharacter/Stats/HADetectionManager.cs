using UnityEngine;

namespace SJ
{
    public class HADetectionManager : MonoBehaviour
    {
        PlayerAttacker playerAttacker;
        public ParticleSystem impactFx;


        void Awake()
        {
            playerAttacker = FindObjectOfType<PlayerAttacker>();
        }
        // Start is called before the first frame update
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 10)
            {
                VaseContainerManager vaseContainerManager = other.GetComponent<VaseContainerManager>();

                if(vaseContainerManager != null)
                {
                    vaseContainerManager.HandleVaseConatinerProcess();
                }
            }

            if(other.gameObject.layer == 11)
            {
                    TreeContainerManager treeContainerManager = other.GetComponent<TreeContainerManager>();
                    treeContainerManager.HandleTreeContainerProcess();
            }

            if(other.gameObject.layer == 12)
            {
                if(other.TryGetComponent<EnemyManager>(out EnemyManager component))
                {
                    Vector3 impactPosition = other.gameObject.transform.position + new Vector3 (0, 1f, 0f);

                    Instantiate(impactFx, impactPosition, Quaternion.identity);
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
                        keliperManager.TakeDamage(playerAttacker.statesJiataData.d_HighAttack);
                        if(keliperManager.keliperPattern.currentTarget == null) 
                        {
                            keliperManager.keliperPattern.currentTarget = FindObjectOfType<PlayerManager>();
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

