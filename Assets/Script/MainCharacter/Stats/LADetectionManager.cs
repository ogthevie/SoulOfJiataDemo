using UnityEngine;

namespace SJ
{
    public class LADetectionManager : MonoBehaviour
    {
        PlayerAttacker playerAttacker;
        public ParticleSystem impactFx;

        void Awake()
        {
            playerAttacker = FindObjectOfType<PlayerAttacker>();
        }

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

            else if(other.gameObject.layer == 11)
            {
                    TreeContainerManager treeContainerManager = other.GetComponent<TreeContainerManager>();
                    treeContainerManager.HandleTreeContainerProcess();
            }

            else if(other.gameObject.layer == 12)
            {
                if(other.TryGetComponent<EnemyManager>(out EnemyManager component))
                {
                    Instantiate(impactFx, component.transform.position, Quaternion.identity);
                    if(component is TololManager tololManager)
                    {
                        tololManager.TakeDamage(playerAttacker.statesJiataData.d_LowAttack);
                        playerAttacker.isHit = true;
                    }
                    else if(component is KossiManager kossiManager)
                    {
                        kossiManager.TakeDamage(playerAttacker.statesJiataData.d_LowAttack);
                    }
                    else if(component is kossiKazeManager kossiKazeManager)
                    {
                        kossiKazeManager.kossiKazePattern.HandleExplosion();
                    }
                }
            }           
        }
    }
}

