using UnityEngine;
using UnityEngine.AI;
using SJ;


public class TololPattern : MonoBehaviour
{
    PlayerAttacker playerAttacker;
    PlayerManager playerManager;
    public StatesCharacterData statesJiataData;
    [SerializeField] TololAnimatorManager tololAnimatorManager;
    TololManager tololManager;
    HandleDamageTolol handleDamageTolol;
    public MobData tololData;
    public PlayerManager currentTarget;
    public NavMeshAgent agentTolol;
    public Rigidbody tololRigibody;
    public LayerMask detectionLayer;
    public float distanceFromTarget;
    public float maxDistanceFromTarget = 30;
    float stoppingDistance = 2.75f;
    float rotationSpeed = 45f;

    public float timeAttack;
    public Collider collidertolol;



    void OnEnable()
    {
        tololRigibody = GetComponent<Rigidbody>();
        tololManager = GetComponent<TololManager>();        
    }
    
    void Start()
    {
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        playerManager = FindObjectOfType<PlayerManager>();
        tololAnimatorManager = GetComponent<TololAnimatorManager>();
        handleDamageTolol = GetComponentInChildren<HandleDamageTolol>();
        tololRigibody.isKinematic = false;
        collidertolol.enabled = false;
        currentTarget = playerManager;
        tololManager.isPreformingAction = false;
        tololManager.isbreak = false;
    }
    
    void Update()
    {
        float delta = Time.deltaTime;
        //Debug.Log(timeAttack);
        HandleTimerAttack(delta);
    }


    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, tololManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null)
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

    }

    public void HandleMoveToTarget()
    {
        if(currentTarget != null)
        {
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
        if(currentTarget == null)
        {
            currentTarget = FindObjectOfType<PlayerManager>();
            tololManager.isPreformingAction = false;
        }
    }

    public void HandleActiveFirstAttack()
    {
        if(collidertolol != null) collidertolol.enabled = true;
        handleDamageTolol.FixDamage(tololData.mobFAtt);
    }

    public void HandleActiveSecAttack()
    {
        if(collidertolol != null) collidertolol.enabled = true;
        handleDamageTolol.FixDamage(tololData.mobSAtt);
    }

    public void CloseHitBox()
    {
        if(collidertolol != null) collidertolol.enabled = false;
    }

    public void UpdateHitting()
    {
        playerAttacker.isHit = false;
        CloseHitBox();
    }

    public void UpdateState()
    {
        tololAnimatorManager.anim.SetBool("canAttack", true);
        tololAnimatorManager.anim.SetBool("isACHit", false);
        UpdateHitting();
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

    public void BreakPoint()
    {
        if(tololManager.isbreak)
        {
            agentTolol.speed = 0;
            tololRigibody.isKinematic = true;
                     
        }
        else
        {
            agentTolol.speed = 3.5f;
            tololRigibody.isKinematic = false;  
        }
    }
}
