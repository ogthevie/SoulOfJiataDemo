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
                    Instantiate(impactFx, component.transform.position, Quaternion.identity);
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

                }

                //jouer l'animation du choc
            }
        }

    }
}

