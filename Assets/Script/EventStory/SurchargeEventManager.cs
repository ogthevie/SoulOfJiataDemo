using UnityEngine;

public class SurchargeEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;

    void Start()
    {
        bomboktanManager = FindObjectOfType<BomboktanManager>();

        if(playerManager.haveGauntlet)
        {
            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this);
        }
    }
    
    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3 && !playerManager.haveGauntlet)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.haveGauntlet = true;
            playerManager.HandleSurchargeBrassard();
            
            StartCoroutine(gameManager.StartHandleAchievement("--Le second brassard--"));
            grotteKossiManager.enemySpawnOne.SetActive(true);
            
            storyManager.storyStep = 3;
            bomboktanManager.Spawn(0);
            
            Invoke("Save", 3f);

            Destroy(this.transform.GetChild(0).gameObject, 4);
            Destroy(this, 30f);
        }
    }

}
