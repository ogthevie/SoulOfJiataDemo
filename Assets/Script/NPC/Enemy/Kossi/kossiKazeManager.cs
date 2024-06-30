using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class kossiKazeManager : EnemyManager
{
    public kossiKazePattern kossiKazePattern;
    PlayerStats playerStats;
    public bool isPreformingAction;
    public readonly float maximumDetectionAngle =  180;
    public readonly float minimumDetectionAngle = -180;
    readonly int kamikazeDamage = 24;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        cameraManager = FindObjectOfType<CameraManager>();
        kossiKazePattern = GetComponent<kossiKazePattern>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        
        isPreformingAction = true;
        
        detectionRadius = 25f;
    }
    
    private void Update() 
    {
        HandleCurrentAction();
        HandleDeath();            
    }

    void HandleCurrentAction()
    {
        /*if(kossiKazePattern.currentTarget == null)
        {
            kossiKazePattern.HandleDetection();
        }
        else*/
        kossiKazePattern.HandleMoveToTarget();

    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
    
    protected override void HandleDeath()
    {
        if(isDead) 
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            foreach(var elt in colliders)
            {
                if(elt.gameObject.layer == 3) playerStats.TakeDamage(kamikazeDamage, 0);

                else if(elt.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager vaseContainerManager)) vaseContainerManager.HandleVaseConatinerProcess();

                else if(elt.gameObject.TryGetComponent<BuffaloManager>(out BuffaloManager buffaloManager))
                {
                    buffaloManager.iStun = true;
                    buffaloManager.TakeDamage(50);
                } 

                else if(elt.gameObject.TryGetComponent<kossiKazePattern>(out kossiKazePattern kossipattern)) kossipattern.HandleExplosion();
                else if(elt.gameObject.TryGetComponent<TololManager>(out TololManager tololManager)) tololManager.TakeDamage(45);
            }
            
            LoadConsumable(consumable);
            cameraManager.ClearLockOnTargets();
            Destroy(this.gameObject);
        }
    }
}
