using UnityEngine;
using SJ;

public class KossiManager : EnemyManager
{
    public KossiPattern kossiPattern;
    EnemyHealthBar enemyHealthBar;
    public GameObject kossiHealthBar;
    //Vector3 tp;
    public bool isPreformingAction;
    public float maximumDetectionAngle;
    public float minimumDetectionAngle;
    public int kossiHealth;

    private void Awake() 
    {
        cameraManager = FindObjectOfType<CameraManager>();
        kossiPattern = GetComponent<KossiPattern>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();        
    }
    private void Start()
    {
        kossiHealthBar = transform.Find("UI Ennemy").gameObject;

        isPreformingAction = true;
        maximumDetectionAngle = 90;
        minimumDetectionAngle = -90;
        detectionRadius = 30f;
        kossiHealth = 8;

        currentHealth = kossiHealth;
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

    void HandleCurrentAction()
    {
        if(kossiPattern.currentTarget == null)
        {
            kossiPattern.HandleDetection();
        }
        else
        kossiPattern.HandleMoveToTarget();

    }
    public override void TakeDamage(int damage)
    {

        currentHealth -= damage;
        //Debug.Log(currentHealth);

        enemyHealthBar.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            kossiPattern.agentKossi.enabled = false;
            isDead = true;
        }
    }
    protected override void HandleDeath()
    {
        if(isDead) 
        {
            kossiPattern.invokeAttack = false;
            Destroy(kossiHealthBar);
        }
        base.HandleDeath();
    }
    public void HandleHealthRenderer()
    {
        if(playerAttacker.cameraManager.currentLockOnTarget == this) kossiHealthBar.SetActive(true);
        else 
        {
            if(kossiHealthBar != null) kossiHealthBar.SetActive(false);
        }
    }
}
