using UnityEngine;
using SJ;

public class TololNoiseManager : EnemyManager
{
    public TololNoiseAnimatorManager tololNoiseAnimatorManager;
    public TololNoisePattern tololNoisePattern;
    TololNoiseAudioManager tololNoiseAudioManager;
    EnemyHealthBar enemyHealthBar;
    public GameObject tololNoiseHealthBar;
    PlayerAttacker playerAttacker;
    public bool isPreformingAction;
    public float maximumDetectionAngle = 180;
    public float minimumDetectionAngle = -120;
    
    private void Start() 
    {
        cameraManager = FindObjectOfType<CameraManager>();
        tololNoisePattern = GetComponent<TololNoisePattern>();
        tololNoiseAnimatorManager = GetComponent<TololNoiseAnimatorManager>();
        tololNoiseAudioManager = GetComponent<TololNoiseAudioManager>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        tololNoiseHealthBar = transform.Find("UI Ennemy").gameObject;

        isPreformingAction = true;

        //detectionRadius = 30f;
        currentHealth = tololNoisePattern.tololData.mobHealth;
        enemyHealthBar.SetMaxHealth(currentHealth);
    }
    private void Update() 
    {
        HandleCurrentAction();
        HandleDeath();
    }

    private void LateUpdate()
    {

        HandleHealthRenderer();
    }

    private void HandleCurrentAction()
    {
        /*if(tololPattern.currentTarget == null)
        {
            tololPattern.HandleDetection();
        }
        else*/
        tololNoisePattern.HandleMoveToTarget();

        if(!isDead) 
            if(playerAttacker.isTargetHit) 
                    tololNoiseAnimatorManager.anim.SetBool("isACHit", true);

    }

    public override void TakeDamage(int damage)
    {

        currentHealth -= damage;
        //Debug.Log(currentHealth);

        enemyHealthBar.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            tololNoisePattern.agentTololNoise.enabled = false;
            isDead = true;
        }
    }

    public void HandleHealthRenderer()
    {
        if(playerAttacker.cameraManager.currentLockOnTarget == this) tololNoiseHealthBar.SetActive(true);
        else 
        {
            if(tololNoiseHealthBar != null) tololNoiseHealthBar.SetActive(false);
        }
    }

    protected override void HandleDeath()
    {

        if(isDead) 
        {
            Destroy(tololNoiseHealthBar);
            tololNoiseAudioManager.ReadDead();
        }
        base.HandleDeath();
    }
}
