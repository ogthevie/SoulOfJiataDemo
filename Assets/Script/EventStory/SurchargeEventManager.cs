using UnityEngine;

public class SurchargeEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;

    void Start()
    {
        bomboktanManager = FindFirstObjectByType<BomboktanManager>();
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && !playerManager.canSurcharge)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canSurcharge = true;
            StartCoroutine(gameManager.StartHandleAchievement("La main du souverain"));

            storyManager.storyStep = 3;
            StartCoroutine(bomboktanManager.SpawnBomboktan(0));
            GetComponent<ParticleSystem>().Stop();
            Invoke("Save", 3f);

            Destroy(this.gameObject, 5f);
        }
    }

}
