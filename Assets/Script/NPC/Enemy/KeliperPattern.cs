using UnityEngine;
using SJ;
using UnityEngine.AI;

public class KeliperPattern : MonoBehaviour
{
    public KeliperAnimatorManager keliperAnimatorManager;
    public PlayerManager currentTarget, playerManager;
    KeliperManager keliperManager;
    public StatesCharacterData statesJiataData;
    public GameObject bulletPrefab;
    public LayerMask detectionLayer;
    public float distanceFromTarget;
    public NavMeshAgent agentKeliper;
    public Rigidbody keliperRigibody;
    GameObject projectile;
    [SerializeField] Transform spawnPoint;
    Rigidbody bulletPrefabRigibody;
    [SerializeField] Vector3 decal= new Vector3 (0, 0.5f, 0);
    public float stoppingDistance;
    float rotationSpeed = 150f;
    public bool bulletAttack, stunt;
    public float viewableAngle;


    void Awake()
    {
        keliperManager = GetComponent<KeliperManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        keliperAnimatorManager = GetComponent<KeliperAnimatorManager>();
        keliperRigibody = GetComponent<Rigidbody>();
        agentKeliper = GetComponentInChildren<NavMeshAgent>();
    }

    void Start()
    {
        stoppingDistance = 5;
        agentKeliper.enabled = false;
        keliperManager.isPreformingAction = false;
        keliperRigibody.isKinematic = false;
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, keliperManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;

                currentTarget = playerManager;
                keliperManager.isPreformingAction = false;
            }
        }
    }

    public void HandleMoveToTarget()
    {
        if (currentTarget.isDead)
            return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        if (keliperManager.isPreformingAction)
        {
            keliperAnimatorManager.anim.SetFloat("run", 0);
            agentKeliper.enabled = false;
        }
        else
        {
            if (keliperManager.currentHealth == 0) return;

            if (distanceFromTarget > stoppingDistance)
            {
                keliperAnimatorManager.anim.SetFloat("run", 1);
                bulletAttack = true;
            }
            else if (distanceFromTarget <= stoppingDistance)
            {
                keliperAnimatorManager.anim.SetFloat("run", 0);
                bulletAttack = true;
            }

            HandleRotateTowardsTarget();
        }

        // Conservez votre code d'origine pour la position et la rotation de l'agentKeliper (en supposant qu'il soit toujours nécessaire)
    }

    void HandleRotateTowardsTarget()
    {
        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }


    void HandleStopChase()
    {
        if(currentTarget.isDead)/*distanceFromTarget >= maxDistanceFromTarget ||*/
        {
            currentTarget = null;
            keliperAnimatorManager.anim.SetFloat("run", 0);
            keliperManager.isPreformingAction = true;
            agentKeliper.enabled = false;
            bulletAttack = false;
        }
        else
        {
            if(currentTarget == null)
            {
                currentTarget = playerManager;
                keliperManager.isPreformingAction = false;
            }
        }
    }

    private void HandleBulletAttack()
    {
        HandleRotateTowardsTarget();
        projectile = Instantiate(bulletPrefab, spawnPoint.position, bulletPrefab.transform.rotation);
        Vector3 targetBullet = currentTarget.transform.GetChild(2).position;

        if(currentTarget != null) 
        {
            Vector3 direction = (currentTarget.transform.position - spawnPoint.position).normalized; // A corriger
            
            bulletPrefabRigibody = projectile.GetComponent<Rigidbody>();
            bulletPrefabRigibody.AddForce(direction * 60f, ForceMode.Impulse);
        }
    }

    public void DisableStunt()
    {
        stunt = false;
    }

    public void BreakPoint()
    {
        if(keliperManager.isbreak)
        {
            agentKeliper.speed = 0;
            keliperRigibody.isKinematic = true;
        }
        else
        {
            agentKeliper.speed = 12f;
            keliperRigibody.isKinematic = false;
        }
    }
}
