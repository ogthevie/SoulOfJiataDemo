using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SJ;


public class TololPattern : MonoBehaviour
{
    PlayerAttacker playerAttacker;
    PlayerManager playerManager;
    public StatesCharacterData statesJiataData;
    public TololAnimatorManager tololAnimatorManager;
    TololManager tololManager;
    HandleDamageTolol handleDamageTolol;
    public MobData tololData;
    public PlayerManager currentTarget;
    public NavMeshAgent agentTolol;
    public Rigidbody tololRigibody;
    //public LayerMask detectionLayer;
    public float distanceFromTarget;
    public float maxDistanceFromTarget = 30;
    float stoppingDistance = 2.75f;
    float rotationSpeed = 45f;

    public float timeAttack;
    public new Collider collider;


    void Start()
    {
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        playerManager = FindObjectOfType<PlayerManager>();
        tololAnimatorManager = GetComponent<TololAnimatorManager>();
        tololManager = GetComponent<TololManager>();
        tololRigibody = GetComponent<Rigidbody>();
        agentTolol = GetComponentInChildren<NavMeshAgent>();
        handleDamageTolol = GetComponentInChildren<HandleDamageTolol>();
        agentTolol.enabled = false;
        tololRigibody.isKinematic = false;
        collider.enabled = false;
        currentTarget = playerManager;
        tololManager.isPreformingAction = false;
    }

    void LateUpdate()
    {
        tololAnimatorManager.anim.SetBool("isHit", playerAttacker.isHit);
        //tololAnimatorManager.anim.SetBool("canAttack", false);
    }

    void Update()
    {
        float delta = Time.deltaTime;
        //Debug.Log(timeAttack);
        HandleTimerAttack(delta);
    }


    /*public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, tololManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null && !statesJiataData.isHidden)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > tololManager.minimumDetectionAngle && viewableAngle < tololManager.maximumDetectionAngle)
                {
                    currentTarget = playerManager;
                    tololManager.isPreformingAction = false;
                }

            }
        }

    }*/

    public void HandleMoveToTarget()
    {
        if(currentTarget.isDead)
            return;
        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        if(tololManager.isPreformingAction)
        {
            tololAnimatorManager.anim.SetFloat("vertical", 0, 0.1f, Time.deltaTime);
            agentTolol.enabled = false;
        }
        else
        {
            if(tololManager.currentHealth == 0) return;

            if(distanceFromTarget > stoppingDistance)
            {
                tololAnimatorManager.anim.SetFloat("vertical", 1, 0.1f, Time.deltaTime);
            }
            else if(distanceFromTarget <= stoppingDistance)
            {
                tololAnimatorManager.anim.SetFloat("vertical", 0, 0.1f, Time.deltaTime);
            }
            HandleRotateTowardsTarget();

        }
        transform.position = new Vector3(transform.position.x, agentTolol.transform.position.y, transform.position.z);
        agentTolol.transform.localPosition = Vector3.zero;
        agentTolol.transform.localRotation = Quaternion.identity;
        HandleStopChase();

        tololAnimatorManager.anim.SetBool("isHit", playerAttacker.isHit);
    }

    private void HandleRotateTowardsTarget()
    {
        //Vector3 relativeDirection = transform.InverseTransformDirection(agentTolol.desiredVelocity);
        Vector3 targetVelocity = tololRigibody.velocity;
        
        agentTolol.enabled = true;
        agentTolol.SetDestination(currentTarget.transform.position);
        tololRigibody.velocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, agentTolol.transform.rotation, rotationSpeed/Time.deltaTime);
    }

    public void HandleStopChase()
    {
        if(statesJiataData.isHidden) //distanceFromTarget >= maxDistanceFromTarget ||   -- xa peut etre utile
        {
            currentTarget = null;
            tololAnimatorManager.anim.SetFloat("vertical", 0);
            tololManager.isPreformingAction = true;
            agentTolol.enabled = false;
            collider.enabled = false;
        }
        else
        {
            if(currentTarget == null)
            {
                currentTarget = playerManager;
                tololManager.isPreformingAction = false;
            }
        }
    }

    public void HandleActiveFirstAttack()
    {
        if(collider != null) collider.enabled = true;
        handleDamageTolol.FixDamage(tololData.mobFAtt);
    }

    public void HandleActiveSecAttack()
    {
        if(collider != null) collider.enabled = true;
        handleDamageTolol.FixDamage(tololData.mobSAtt);
    }

    public void CloseHitBox()
    {
        if(collider != null) collider.enabled = false;
    }

    public void UpdateHitting()
    {
        playerAttacker.isHit = false;
    }

    public void UpdateState()
    {
        tololAnimatorManager.anim.SetBool("canAttack", true);
        tololAnimatorManager.anim.SetBool("isACHit", false);
        playerAttacker.isTargetHit = false;
    }

    void HandleTimerAttack(float delta)
    {
        if(playerAttacker.isHit)
        {
            timeAttack = 0;
            return;
        }

        timeAttack += delta;

        if(timeAttack > 0.8f) timeAttack = 0f;
    }
}
