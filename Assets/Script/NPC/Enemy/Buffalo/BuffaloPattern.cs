using UnityEngine;
using SJ;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;


public class BuffaloPattern : MonoBehaviour
{
    public BuffaloAnimatorManager buffaloAnimatorManager;
    public BuffaloAudioManager buffaloAudioManager;
    [SerializeField] HandleDamageTolol handleDamageTolol;
    public PlayerManager currentTarget, playerManager;
    BuffaloManager buffaloManager;
    public StatesCharacterData statesJiataData;
    public LayerMask detectionLayer;
    public float distanceFromTarget, stoppingDistance, timer, stunDelay;
    [SerializeField]float hellbowDelay = 5;
    public int indexDistance, indexAttack;
    [SerializeField] List<float> duration = new List<float>() { 2.5f, 5f, 7f };
    public NavMeshAgent agentBuffalo;
    public Rigidbody buffaloRigidbody;
    float rotationSpeed = 400f;
    public GameObject bulletPrefab;
    GameObject projectile;
    Rigidbody bulletPrefabRigibody;
    [SerializeField] Vector3 decal= new Vector3 (0, 0.5f, 0);
    public bool bulletAttack;
    public GameObject spawnRage_one, spawnRage_two, stele;
    [SerializeField] EnemyManager[] enemyManagers;
    public bool canAttack;
    [SerializeField] ParticleSystem hellSword;
    public RaycastHit targetHit;
    [SerializeField] BoxCollider swordBox;
    void Awake()
    {
        buffaloManager = GetComponent<BuffaloManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        buffaloAudioManager = GetComponent<BuffaloAudioManager>();
        buffaloAnimatorManager = GetComponent<BuffaloAnimatorManager>();
        buffaloRigidbody = GetComponent<Rigidbody>();
        agentBuffalo = GetComponentInChildren<NavMeshAgent>();
        swordBox.enabled = false;
    }
    void Start()
    {
        //maxDistanceFromTarget = 35;
        stoppingDistance = 5;
        //currentTarget = playerManager;
        buffaloRigidbody.isKinematic = false;
        handleDamageTolol.FixDamage(40);
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, buffaloManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponentInParent<PlayerManager>();

            if(playerManager != null)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;
                currentTarget = playerManager;
                agentBuffalo.enabled = true;
                canAttack = true;
            }
        }
    }

    public void HandleMoveToTarget()
    {
        if(currentTarget.isDead)
            return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        //if(indexAttack != 0) return;

        if(buffaloManager.currentHealth == 0) return;

            /*if(distanceFromTarget > stoppingDistance)
            {
                buffaloAnimatorManager.anim.SetFloat("run", 1);
            }
            else if(distanceFromTarget <= stoppingDistance)
            {
                buffaloAnimatorManager.anim.SetFloat("run", 0);
            }*/
        HandleRotateTowardsTarget();

        transform.position = new Vector3(transform.position.x, agentBuffalo.transform.position.y, transform.position.z);
        agentBuffalo.transform.localPosition = Vector3.zero;
        agentBuffalo.transform.localRotation = Quaternion.identity;

    }

    void HandleRotateTowardsTarget()
    {
        //Vector3 relativeDirection = transform.InverseTransformDirection(agentKossi.desiredVelocity);
        Vector3 targetVelocity = agentBuffalo.velocity;

        if(agentBuffalo.enabled)
        {
            agentBuffalo.SetDestination(currentTarget.transform.position);
            buffaloRigidbody.velocity = targetVelocity;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, agentBuffalo.transform.rotation, rotationSpeed / Time.deltaTime);
    }

    public void HandleCheckTimer()
    {
        if(buffaloManager.isTiming) return;
        timer = duration[Random.Range(0, duration.Count)];
        buffaloManager.isTiming = false;
    }

    public void HandleTimerAttack(float delta)
    {
        if(timer > 0)
        {
            buffaloManager.isTiming = true;
            timer -= delta;
        }
        else 
        {
            timer = 0;
            CheckDistance();
        }
    }

    public void CheckDistance()
    {
        if (distanceFromTarget < 10) indexDistance = 1;
        else if(distanceFromTarget < 37) indexDistance = 2;
        else indexDistance = 3;

        indexAttack = Random.Range(1, 3);
    }

    public void ResetIndex()
    {
        indexAttack = indexDistance = 0;
    }

    public void EnablePlotArmor()
    {
        buffaloManager.plotArmorFx.Play();
        buffaloManager.plotCollider.enabled = true;
        buffaloManager.isArmor = true;
        agentBuffalo.enabled = true;        
    }

    public void DisablePlotArmor()
    {
        buffaloManager.plotArmorFx.Stop();
        buffaloManager.plotCollider.enabled = false;
        buffaloManager.isArmor = false;        
    }

    public void DisableStun(float delta)
    {
        if(buffaloManager.iStun) 
        {
            stunDelay += delta;
            if(stunDelay > 6)
            {
                stunDelay = 0;
                buffaloManager.iStun = false;
            }
        }
    }

    public void EnableHellbow()
    {
        if(!buffaloManager.isHellbow) 
        {
            buffaloManager.isHellbow = true;
        }
    }

    public void HandleHellBow(float delta)
    {
        if(hellbowDelay <= 0)
        {
            buffaloManager.isHellbow = false;
            hellbowDelay = 5;
        }
        else
        {
            hellbowDelay -= delta;
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

            if(buffaloManager.currentHealth == 0) return;

            HandleRotateTowardsTarget();

            transform.position = new Vector3(transform.position.x, agentBuffalo.transform.position.y, transform.position.z);
            agentBuffalo.transform.localPosition = Vector3.zero;
            agentBuffalo.transform.localRotation = Quaternion.identity;
        }
    }

    public void PlayHellSword()
    {
        agentBuffalo.enabled = false;
        hellSword.Play();
    }

    public void HandleHellCollider()
    {

       StartCoroutine(CheckSwordDamage());
       
       IEnumerator CheckSwordDamage()
       {
            swordBox.enabled = true;
            yield return new WaitForSeconds (0.8f);
            swordBox.enabled = false;
       }

    }

    private void HandleBulletAttack()
    {
        if(projectile == null)
        {
            projectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            if(currentTarget != null) 
            {
                Vector3 direction = ((currentTarget.transform.position + decal) - transform.position).normalized; // A corriger
                
                bulletPrefabRigibody = projectile.GetComponent<Rigidbody>();
                bulletPrefabRigibody.AddForce(direction * 100f, ForceMode.Impulse);
            }
        }
        
        if(projectile != null) Destroy(projectile, 2.5f);
    }

    private void HandleRageSpawn()
    {
        if(stele.transform.childCount > 5) return;
        
        if(distanceFromTarget >= 10  || statesJiataData.isHidden) StartCoroutine(Spawnkossikaze());
        else playerManager.playerStats.TakeDamage(15, 1);

        IEnumerator Spawnkossikaze()
        {
            Transform positionOne = spawnRage_one.transform;
            Transform positionTwo = spawnRage_two.transform;

            GameObject visualOne = Instantiate(buffaloManager.enemyExplosion);
            visualOne.transform.SetParent(positionOne);
            visualOne.transform.localPosition = Vector3.zero;
            visualOne.transform.rotation = Quaternion.identity;

            GameObject visualTwo = Instantiate(buffaloManager.enemyExplosion);
            visualTwo.transform.SetParent(positionTwo);
            visualTwo.transform.localPosition = Vector3.zero;
            visualTwo.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(0.2f);

            if(buffaloManager.currentHealth < 180)
            {
                LoadEnemy(spawnRage_one.transform, Random.Range(0,3), stele.transform);
                LoadEnemy(spawnRage_two.transform, Random.Range(0,3), stele.transform);    
            }
            else
            {
                LoadEnemy(spawnRage_one.transform, Random.Range(0,2), stele.transform);
                LoadEnemy(spawnRage_two.transform, Random.Range(0,2), stele.transform); 
            }
            
      
        }
    }

    private void LoadEnemy(Transform tp, int i, Transform balise)
    {
        GameObject visuals = Instantiate(enemyManagers[i].gameObject);
        visuals.transform.SetParent(tp);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
        visuals.transform.SetParent(balise);        
    }

    private void PlaySummonFx()
    {
        buffaloManager.summonKossiFx.Play();
    }
}
