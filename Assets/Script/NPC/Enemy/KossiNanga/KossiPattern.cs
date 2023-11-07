using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;
using UnityEngine.AI;

public class KossiPattern : MonoBehaviour
{
    public KossiAnimatorManager kossiAnimatorManager;
    public KossiAudioManager kossiAudioManager;
    public PlayerManager currentTarget;
    KossiManager kossiManager;
    public StatesCharacterData statesJiataData;
    public GameObject bulletPrefab;
    public LayerMask detectionLayer;
    public float distanceFromTarget;
    public NavMeshAgent agentKossi;
    public Rigidbody kossiRigibody;
    GameObject projectile;
    Transform spawnPoint;
    Rigidbody bulletPrefabRigibody;
    [SerializeField] Vector3 decal= new Vector3 (0, 0.5f, 0);
    public float maxDistanceFromTarget;
    public float stoppingDistance;
    float rotationSpeed = 75f;
    public bool bulletAttack;
    public float viewableAngle;


    void Awake()
    {
        kossiManager = GetComponent<KossiManager>();
        kossiAudioManager = GetComponent<KossiAudioManager>();
        kossiAnimatorManager = GetComponent<KossiAnimatorManager>();
        spawnPoint = transform.GetChild(0);
        kossiRigibody = GetComponent<Rigidbody>();
        agentKossi = GetComponentInChildren<NavMeshAgent>();
    }
    void Start()
    {
        maxDistanceFromTarget = 35;
        stoppingDistance = 10;
        agentKossi.enabled = false;
        kossiRigibody.isKinematic = false;
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, kossiManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null && !statesJiataData.isHidden)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > kossiManager.minimumDetectionAngle && viewableAngle < kossiManager.maximumDetectionAngle)
                {
                    currentTarget = playerManager;
                    kossiManager.isPreformingAction = false;
                    kossiAudioManager.ReadAttackFx();
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
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
                bulletAttack = true;
            }
            else if(distanceFromTarget <= stoppingDistance)
            {
                kossiAnimatorManager.anim.SetFloat("run", 0);
                bulletAttack = true;
            }

            HandleRotateTowardsTarget();
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
        kossiRigibody.velocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, agentKossi.transform.rotation, rotationSpeed / Time.deltaTime);
    }

    void HandleStopChase()
    {
        if(distanceFromTarget >= maxDistanceFromTarget || statesJiataData.isHidden || currentTarget.isDead)
        {
            currentTarget = null;
            kossiAnimatorManager.anim.SetFloat("run", 0);
            kossiManager.isPreformingAction = true;
            agentKossi.enabled = false;
            bulletAttack = false;
        }
    }

    private void HandleBulletAttack()
    {
        if(projectile == null)
        {
            projectile = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

            if(currentTarget != null) 
            {
                Vector3 direction = ((currentTarget.transform.position + decal) - spawnPoint.position).normalized; // A corriger
                
                bulletPrefabRigibody = projectile.GetComponent<Rigidbody>();
                bulletPrefabRigibody.AddForce(direction * 70f, ForceMode.Impulse);
            }
        }
        
        if(projectile != null) Destroy(projectile, 2.5f);
    }

}
