using UnityEngine;
using SJ;
using UnityEngine.AI;
using System.Collections;

public class KossiPattern : MonoBehaviour
{
    public KossiAnimatorManager kossiAnimatorManager;
    public KossiAudioManager kossiAudioManager;
    public PlayerManager currentTarget, playerManager;
    KossiManager kossiManager;
    public StatesCharacterData statesJiataData;
    public GameObject kossiKazePrefab;
    public LayerMask detectionLayer;
    public float distanceFromTarget;
    public NavMeshAgent agentKossi;
    public Rigidbody kossiRigibody;
    GameObject projectile;
    [SerializeField] Transform spawnPoint;
    Rigidbody bulletPrefabRigibody;
    [SerializeField] Vector3 decal= new Vector3 (0, 0.5f, 0);
    public float maxDistanceFromTarget;
    public float stoppingDistance;
    float rotationSpeed = 75f;
    public bool invokeAttack;
    public float viewableAngle;


    void Awake()
    {
        kossiManager = GetComponent<KossiManager>();
        playerManager = FindFirstObjectByType<PlayerManager>();
        kossiAudioManager = GetComponent<KossiAudioManager>();
        kossiAnimatorManager = GetComponent<KossiAnimatorManager>();
        kossiRigibody = GetComponent<Rigidbody>();
        agentKossi = GetComponentInChildren<NavMeshAgent>();
    }
    void Start()
    {
        maxDistanceFromTarget = 35;
        stoppingDistance = 10;
        agentKossi.enabled = false;
        kossiManager.isPreformingAction = false;
        kossiRigibody.isKinematic = false;
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, kossiManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > kossiManager.minimumDetectionAngle && viewableAngle < kossiManager.maximumDetectionAngle)
                {
                    currentTarget = playerManager;
                    kossiManager.isPreformingAction = false;
                    //kossiAudioManager.ReadAttack();
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        if(currentTarget.isDead)
            return;
        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        if(kossiManager.isPreformingAction)
        {
            kossiAnimatorManager.anim.SetFloat("run", 0);
            agentKossi.enabled = false;
        }
        else
        {
            if(kossiManager.currentHealth == 0) return;

            if(distanceFromTarget > stoppingDistance)
            {
                kossiAnimatorManager.anim.SetFloat("run", 1);
                invokeAttack = true;
            }
            else if(distanceFromTarget <= stoppingDistance)
            {
                kossiAnimatorManager.anim.SetFloat("run", 0);
                invokeAttack = true;
            }

            if(!kossiManager.isbreak) HandleRotateTowardsTarget();
        }

        transform.position = new Vector3(transform.position.x, agentKossi.transform.position.y, transform.position.z);
        agentKossi.transform.localPosition = Vector3.zero;
        agentKossi.transform.localRotation = Quaternion.identity;
        HandleStopChase();
    }

    void HandleRotateTowardsTarget()
    {
        //Vector3 relativeDirection = transform.InverseTransformDirection(agentKossi.desiredVelocity);
        Vector3 targetVelocity = agentKossi.velocity;

        agentKossi.enabled = true;
        agentKossi.SetDestination(currentTarget.transform.position);
        kossiRigibody.linearVelocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, agentKossi.transform.rotation, rotationSpeed / Time.deltaTime);
    }

    void HandleStopChase()
    {
        if(currentTarget.isDead)/*distanceFromTarget >= maxDistanceFromTarget ||*/
        {
            currentTarget = null;
            kossiAnimatorManager.anim.SetFloat("run", 0);
            kossiManager.isPreformingAction = true;
            agentKossi.enabled = false;
            invokeAttack = false;
        }
        else
        {
            if(currentTarget == null)
            {
                currentTarget = playerManager;
                kossiManager.isPreformingAction = false;
            }
        }
    }

    private void HandleInvokeAttack()
    {
        kossiAudioManager.InvokeKossiKaze();
        GameObject kami = Instantiate(kossiKazePrefab, spawnPoint.position, Quaternion.identity);
        kami.transform.parent = null;
    }

}
