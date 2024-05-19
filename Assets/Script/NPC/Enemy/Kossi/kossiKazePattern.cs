using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;
using UnityEngine.AI;

public class kossiKazePattern : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerManager currentTarget;
    kossiKazeManager kossiKazeManager;
    public StatesCharacterData statesJiataData;
    KossiKazeAnimatorManager kossiKazeAnimatorManager;
    KazeAudioManager kazeAudioManager;
    public LayerMask detectionLayer;
    public ParticleSystem instability;
    public ParticleSystem KazeExplosion;
    public NavMeshAgent agentKossiKaze;
    public Rigidbody kossiKazeRigibody;
    GameObject projectile;
    [SerializeField] Vector3 decal= new Vector3 (0, 0.5f, 0);
    public float distanceFromTarget;
    public float maxDistanceFromTarget = 20;
    public float stoppingDistance = 4;
    float rotationSpeed = 125f;
    public bool canExplose;
    public float viewableAngle;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        kossiKazeManager = GetComponent<kossiKazeManager>();
        kazeAudioManager = GetComponent<KazeAudioManager>();
        kossiKazeAnimatorManager = GetComponent<KossiKazeAnimatorManager>();
        agentKossiKaze = GetComponentInChildren<NavMeshAgent>();
        agentKossiKaze.enabled = false;
        kossiKazeRigibody.isKinematic = false;
        currentTarget = playerManager;
        kossiKazeManager.isPreformingAction = false;     
    }

    /*public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, kossiKazeManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null && !statesJiataData.isHidden)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > kossiKazeManager.minimumDetectionAngle && viewableAngle < kossiKazeManager.maximumDetectionAngle)
                {
                    currentTarget = playerManager;
                    kossiKazeManager.isPreformingAction = false;
                    instability.Play();
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

        if(kossiKazeManager.isPreformingAction)
        {
            kossiKazeAnimatorManager.anim.SetFloat("run", 0);
            agentKossiKaze.enabled = false;
        }
        else
        {
            if(distanceFromTarget > stoppingDistance)
            {
                if(distanceFromTarget < 15)
                {
                    instability.Play();
                }
                else
                {
                    instability.Stop();
                }
                kossiKazeAnimatorManager.anim.SetFloat("run", 1);
                canExplose = false;
            }
            else if(distanceFromTarget <= stoppingDistance)
            {
                kossiKazeAnimatorManager.anim.SetFloat("run", 0);
                canExplose = true;
            }

            HandleRotateTowardsTarget();
        }

        transform.position = new Vector3(transform.position.x, agentKossiKaze.transform.position.y, transform.position.z);
        agentKossiKaze.transform.localPosition = Vector3.zero;
        agentKossiKaze.transform.localRotation = Quaternion.identity;
        HandleStopChase();
    }

    void HandleRotateTowardsTarget()
    {
        //Vector3 relativeDirection = transform.InverseTransformDirection(agentKossi.desiredVelocity);
        Vector3 targetVelocity = agentKossiKaze.velocity;

        agentKossiKaze.enabled = true;
        agentKossiKaze.SetDestination(currentTarget.transform.position);
        kossiKazeRigibody.velocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, agentKossiKaze.transform.rotation, rotationSpeed / Time.deltaTime);
    }

    void HandleStopChase()
    {
        if(currentTarget.isDead) //distanceFromTarget >= maxDistanceFromTarget
        {
            currentTarget = null;
            kossiKazeAnimatorManager.anim.SetFloat("run", 0);
            kossiKazeManager.isPreformingAction = true;
            agentKossiKaze.enabled = false;
        }
        else
        {
            if(currentTarget == null)
            {
                currentTarget = playerManager;
                kossiKazeManager.isPreformingAction = false;
            }
        }
    }

    public void HandleExplosion()
    {
        Instantiate(KazeExplosion, this.gameObject.transform.position, Quaternion.identity);
        kazeAudioManager.ReadDead();
        instability.Stop();
        kossiKazeManager.isDead = true;
    }

}
