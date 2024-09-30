using UnityEngine;
using SJ;
using System.Collections.Generic;

public abstract class EnemyManager : MonoBehaviour
{
    protected CameraManager cameraManager;
    public float detectionRadius;
    public bool isDead, isbreak;
    public Transform lockOnTransform;
    public int currentHealth;
    public GameObject enemyExplosion;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    protected PlayerAttacker playerAttacker;
    public List <Material> enemyRendererSection = new();


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

    public virtual void LoadConsumable(GameObject consumable)
    {
        GameObject visuals = Instantiate(consumable, this.gameObject.transform.position, Quaternion.identity);   
    }
}
