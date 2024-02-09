using UnityEngine;
using UnityEngine.AI;
using SJ;
using System.Collections;

public class TololNoisePattern : MonoBehaviour
{
    PlayerAttacker playerAttacker;
    PlayerManager playerManager;
    public StatesCharacterData statesJiataData;
    public TololNoiseAnimatorManager tololNoiseAnimatorManager;
    TololNoiseManager tololNoiseManager;
    TololNoiseAudioManager tololNoiseAudioManager;
    HandleDamageTolol handleDamageTolol;
    protected CameraShake cameraShake;
    public MobData tololData;
    public PlayerManager currentTarget;
    public NavMeshAgent agentTololNoise;
    public Rigidbody tololNoiseRigibody;
    //public LayerMask detectionLayer;
    public float distanceFromTarget;
    public float maxDistanceFromTarget = 30;
    float stoppingDistance = 2.75f;
    float rotationSpeed = 45f;

    public float timeAttack;
    public new Collider collider;
    public SphereCollider medusaCollider;
    [SerializeField] GameObject medusaSong;



    void OnEnable()
    {
        tololNoiseRigibody = GetComponent<Rigidbody>();
        medusaCollider = GetComponent<SphereCollider>();
        medusaCollider.enabled = false;     
    }
    
    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        playerManager = FindObjectOfType<PlayerManager>();
        tololNoiseAnimatorManager = GetComponent<TololNoiseAnimatorManager>();
        tololNoiseManager = GetComponent<TololNoiseManager>();
        tololNoiseAudioManager = GetComponent<TololNoiseAudioManager>();
        agentTololNoise = GetComponentInChildren<NavMeshAgent>();
        handleDamageTolol = GetComponentInChildren<HandleDamageTolol>();
        agentTololNoise.enabled = false;
        tololNoiseRigibody.isKinematic = false;
        collider.enabled = false;
        currentTarget = playerManager;
        tololNoiseManager.isPreformingAction = false;
    }

    void LateUpdate()
    {
        tololNoiseAnimatorManager.anim.SetBool("isHit", playerAttacker.isHit);
        //tololAnimatorManager.anim.SetBool("canAttack", false);
    }

    void Update()
    {
        float delta = Time.deltaTime;
        //Debug.Log(timeAttack);
        HandleTimerAttack(delta);
    }


    void OnTriggerEnter(Collider other)
    {
        cameraShake.Shake(8.5f, 1.25f);
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
        if(currentTarget != null)
        {
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

            if(tololNoiseManager.isPreformingAction)
            {
                tololNoiseAnimatorManager.anim.SetFloat("vertical", 0, 0.1f, Time.deltaTime);
                agentTololNoise.enabled = false;
            }
            else
            {
                if(tololNoiseManager.currentHealth == 0) return;

                if(distanceFromTarget > stoppingDistance)
                {
                    tololNoiseAnimatorManager.anim.SetFloat("vertical", 1, 0.1f, Time.deltaTime);
                }
                else if(distanceFromTarget <= stoppingDistance)
                {
                    tololNoiseAnimatorManager.anim.SetFloat("vertical", 0, 0.1f, Time.deltaTime);
                }
                HandleRotateTowardsTarget();

            }
        }

        transform.position = new Vector3(transform.position.x, agentTololNoise.transform.position.y, transform.position.z);
        agentTololNoise.transform.localPosition = Vector3.zero;
        agentTololNoise.transform.localRotation = Quaternion.identity;
        HandleStopChase();

        tololNoiseAnimatorManager.anim.SetBool("isHit", playerAttacker.isHit);
    }

    private void HandleRotateTowardsTarget()
    {
        //Vector3 relativeDirection = transform.InverseTransformDirection(agentTolol.desiredVelocity);
        Vector3 targetVelocity = tololNoiseRigibody.velocity;
        
        agentTololNoise.enabled = true;
        agentTololNoise.SetDestination(currentTarget.transform.position);
        tololNoiseRigibody.velocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, agentTololNoise.transform.rotation, rotationSpeed/Time.deltaTime);
    }

    public void HandleStopChase()
    {
        if(statesJiataData.isHidden) //distanceFromTarget >= maxDistanceFromTarget ||   -- xa peut etre utile
        {
            currentTarget = null;
            tololNoiseAnimatorManager.anim.SetFloat("vertical", 0);
            tololNoiseManager.isPreformingAction = true;
            agentTololNoise.enabled = false;
            collider.enabled = false;
        }
        else
        {
            if(currentTarget == null)
            {
                currentTarget = FindObjectOfType<PlayerManager>();
                tololNoiseManager.isPreformingAction = false;
            }
        }
    }

    public void HandleScreamingAttack()
    {

        StartCoroutine(MedusaSing());

        IEnumerator MedusaSing()
        {
            medusaSong.SetActive(true);
            medusaCollider.enabled = true;
            tololNoiseAudioManager.ReadAttack();
            yield return new WaitForSeconds(4.5f);

            medusaCollider.enabled = false;
            medusaSong.SetActive(false);
        }

        
    }

    public void UpdateHitting()
    {
        playerAttacker.isHit = false;
    }

    public void UpdateState()
    {
        tololNoiseAnimatorManager.anim.SetBool("canAttack", true);
        tololNoiseAnimatorManager.anim.SetBool("isACHit", false);
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

        if(timeAttack > 10f) timeAttack = 0f;
    }
}
