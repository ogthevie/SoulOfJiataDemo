using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public abstract class EnemyManager : MonoBehaviour
{
    protected CameraManager cameraManager;
    public float detectionRadius;
    public bool isDead;
    public Transform lockOnTransform;
    public int currentHealth;
    public GameObject enemyExplosion;
    public SkinnedMeshRenderer skinnedMeshRenderer;


    public abstract void TakeDamage(int damage);
    
    protected virtual void HandleDeath()
    {
        if(isDead)
        {
            Instantiate(enemyExplosion, this.gameObject.transform.position, Quaternion.identity);
            skinnedMeshRenderer.enabled = false;
            cameraManager.ClearLockOnTargets();
            Destroy(this.gameObject);
        }
    }
    
}
