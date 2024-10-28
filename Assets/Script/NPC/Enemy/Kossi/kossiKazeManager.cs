using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class kossiKazeManager : EnemyManager
{
    public kossiKazePattern kossiKazePattern;
    PlayerStats playerStats;
    
    [SerializeField] GameObject awakeFx;
    public bool isPreformingAction;
    public readonly float maximumDetectionAngle =  180;
    public readonly float minimumDetectionAngle = -180;
    readonly int kamikazeDamage = 24;

    private void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        cameraManager = FindFirstObjectByType<CameraManager>();
        kossiKazePattern = GetComponent<kossiKazePattern>();
        playerAttacker = FindFirstObjectByType<PlayerAttacker>();
        awakeFx.SetActive(true);
        
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
                if(elt.gameObject.layer == 3)
                {
                    float distance = Vector3.Distance(transform.position, elt.gameObject.transform.position);
                    if(distance <= 12) playerStats.TakeDamage(kamikazeDamage, 0);
                } 

                else if(elt.gameObject.TryGetComponent<VaseContainerManager>(out VaseContainerManager vaseContainerManager)) vaseContainerManager.HandleVaseConatinerProcess();
                else if(elt.gameObject.TryGetComponent<TololManager>(out TololManager tololManager)) tololManager.TakeDamage(45);
            }
            
            cameraManager.ClearLockOnTargets();
            Destroy(this.gameObject);
        }
    }
}
