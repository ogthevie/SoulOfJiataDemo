using UnityEngine;
using SJ;

public class KeliperManager : EnemyManager
{
    public KeliperPattern keliperPattern;
    EnemyHealthBar enemyHealthBar;
    public GameObject keliperHealthBar, awakeFx;
    //Vector3 tp;
    public bool isPreformingAction;
    public int keliperHealth;

    private void Awake() 
    {
        cameraManager = FindObjectOfType<CameraManager>();
        keliperPattern = GetComponent<KeliperPattern>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();        
    }

    private void Start()
    {
        awakeFx.SetActive(true);
        keliperHealthBar = transform.Find("UI Ennemy").gameObject;

        isPreformingAction = true;
        detectionRadius = 45f;
        keliperHealth = 46;

        currentHealth = keliperHealth;
        enemyHealthBar.SetMaxHealth(currentHealth);
    }

    private void Update() 
    {
        HandleCurrentAction();
        HandleDeath();            
    }

    private void LateUpdate() 
    {
        keliperPattern.BreakPoint();
        HandleHealthRenderer();
    }

    void HandleCurrentAction()
    {
        if(isbreak) return;
        if(keliperPattern.currentTarget == null)
        {
            keliperPattern.HandleDetection();
        }
        else
        keliperPattern.HandleMoveToTarget();

    }

    public override void TakeDamage(int damage)
    {

        currentHealth -= damage;
        //Debug.Log(currentHealth);

        enemyHealthBar.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            keliperPattern.agentKeliper.enabled = false;
            isDead = true;
        }
    }

    protected override void HandleDeath()
    {
        if(isDead) 
        {
            LoadConsumable(consumable);
            keliperPattern.bulletAttack = false;
            Destroy(keliperHealthBar);
        }
        base.HandleDeath();
    }

    public void HandleHealthRenderer()
    {
        if(playerAttacker.cameraManager.currentLockOnTarget == this) keliperHealthBar.SetActive(true);
        else 
        {
            if(keliperHealthBar != null) keliperHealthBar.SetActive(false);
        }
    }
}
