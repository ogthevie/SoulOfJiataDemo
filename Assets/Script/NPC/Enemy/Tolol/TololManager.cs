using UnityEngine;
using SJ;

public class TololManager : EnemyManager //LES ENNEMIS NE SONT PAS CENSES SE RETROUVER SUR LES PLATEFORMES ELEVEES
{
    public TololAnimatorManager tololAnimatorManager;
    public TololPattern tololPattern;
    TololAudioManager tololAudioManager;
    EnemyHealthBar enemyHealthBar;
    public GameObject tololHealthBar, awakeFx;
    public GameObject epeeTolol;
    public bool isPreformingAction;
    public float maximumDetectionAngle = 180;
    public float minimumDetectionAngle = -120;
    
    private void Start() 
    {
        cameraManager = FindFirstObjectByType<CameraManager>();
        tololPattern = GetComponent<TololPattern>();
        tololAnimatorManager = GetComponent<TololAnimatorManager>();
        tololAudioManager = GetComponent<TololAudioManager>();
        playerAttacker = FindFirstObjectByType<PlayerAttacker>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        tololHealthBar = transform.Find("UI Ennemy").gameObject;

        isPreformingAction = true;
        isbreak = false;

        detectionRadius = 22f;
        currentHealth = tololPattern.tololData.mobHealth;
        enemyHealthBar.SetMaxHealth(currentHealth);
        awakeFx.SetActive(true);
    }
    private void Update() 
    {
        HandleCurrentAction();
        HandleDeath();
    }

    private void LateUpdate()
    {
        tololPattern.BreakPoint();
        HandleHealthRenderer();
    }

    private void HandleCurrentAction()
    {
        if(isbreak) return;
        if(tololPattern.currentTarget == null)
        {
            tololPattern.HandleDetection();
        }
        else
        tololPattern.HandleMoveToTarget();

        if(!isDead) 
            if(playerAttacker.isTargetHit) 
                    tololAnimatorManager.anim.SetBool("isACHit", true);

    }

    public override void TakeDamage(int damage)
    {

        currentHealth -= damage;
        //Debug.Log(currentHealth);

        enemyHealthBar.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            tololPattern.agentTolol.enabled = false;
            isDead = true;
        }
    }

    public void HandleHealthRenderer()
    {
        if(playerAttacker.cameraManager.currentLockOnTarget == this) tololHealthBar.SetActive(true);
        else 
        {
            if(tololHealthBar != null) tololHealthBar.SetActive(false);
        }
    }

    protected override void HandleDeath()
    {
        if(isDead) 
        {
            Destroy(epeeTolol);
            Destroy(tololHealthBar);
            tololAudioManager.ReadDead();
        }
        base.HandleDeath();
    }
}
