using UnityEngine;

public class ArcLightEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindFirstObjectByType<BomboktanManager>();
        if(playerManager.canArcLight) Destroy(this.gameObject, 0.5f);
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 3 && !playerManager.canArcLight)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canArcLight = true;
            
            StartCoroutine(gameManager.StartHandleAchievement("Le souffle de Shango"));
            GetComponent<ParticleSystem>().Stop();
            StartCoroutine(bomboktanManager.SpawnBomboktan(1));
            Invoke("Save", 3f);
            Destroy(this.gameObject, 5f);
        }            
    }
}
