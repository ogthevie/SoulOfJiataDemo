using UnityEngine;

public class ThunderEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindFirstObjectByType<BomboktanManager>();
        if(playerManager.canThunder) Destroy(this.gameObject, 0.5f);
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 3 && !playerManager.canThunder)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canThunder = true;
            
            StartCoroutine(gameManager.StartHandleAchievement("Le cri du ciel"));
            StartCoroutine(tutoManager.HandleDescriptionSkillTutos(0,2));
            GetComponent<ParticleSystem>().Stop();
            StartCoroutine(bomboktanManager.SpawnBomboktan(2));
            Invoke("Save", 3f);
            Destroy(this.gameObject, 15f);
        }            
    }

}
