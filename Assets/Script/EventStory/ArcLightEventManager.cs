using UnityEngine;

public class ArcLightEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindObjectOfType<BomboktanManager>();
        if(playerManager.canArcLight) Destroy(this.gameObject, 0.5f);
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 3 && !playerManager.canArcLight)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canArcLight = true;
            
            StartCoroutine(gameManager.StartHandleAchievement("LE SOUFFLE DE SHANGO"));
            GetComponent<ParticleSystem>().Stop();
            Invoke("Save", 3f);
            Destroy(this.gameObject, 5f);
        }            
    }
}
