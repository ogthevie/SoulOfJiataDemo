using UnityEngine;

public class ArcLightEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        if(playerManager.canArcLight)
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(this);
        }

        bomboktanManager = FindObjectOfType<BomboktanManager>();
    }
    protected override void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.layer == 3 && !playerManager.canArcLight)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canArcLight = true;
            
            StartCoroutine(gameManager.StartHandleAchievement("--Wuta Lantarki--"));
            grotteKossiManager.enemySpawntwo.SetActive(true);
            
            storyManager.storyStep = 31;
            bomboktanManager.Spawn(1);
            
            Invoke("Save", 6f);

            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this, 30f);
        }            
    }
}
