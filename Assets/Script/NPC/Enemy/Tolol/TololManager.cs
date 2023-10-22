using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class TololManager : EnemyManager //LES ENNEMIS NE SONT PAS CENSES SE RETROUVER SUR LES PLATEFORMES ELEVEES
{
    public TololAnimatorManager tololAnimatorManager;
    public TololPattern tololPattern;
    EnemyHealthBar enemyHealthBar;
    public GameObject tololHealthBar;
    public GameObject epeeTolol;
    PlayerAttacker playerAttacker;
    public bool isPreformingAction;
    public float maximumDetectionAngle = 180;
    public float minimumDetectionAngle = -120;
    
    private void Start() 
    {
        cameraManager = FindObjectOfType<CameraManager>();
        tololPattern = GetComponent<TololPattern>();
        tololAnimatorManager = GetComponent<TololAnimatorManager>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        tololHealthBar = transform.Find("UI Ennemy").gameObject;

        isPreformingAction = true;

        detectionRadius = 22f;
        currentHealth = tololPattern.tololData.mobHealth;
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
        }
        base.HandleDeath();
    }
}
