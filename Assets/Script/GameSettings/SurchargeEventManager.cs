using UnityEngine;

public class SurchargeEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;

    void Start()
    {
        bomboktanManager = FindObjectOfType<BomboktanManager>();

        if(playerManager.canSurcharge)
        {
            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this);
        }
    }
    
    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3 && !playerManager.canSurcharge)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canSurcharge = true;

            playerManager.brasL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            playerManager.brassardL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            //Tutoscreen
            grotteKossiManager.enemySpawnOne.SetActive(true);
            
            storyManager.storyStep = 5;
            bomboktanManager.Spawn(0);
            
            Invoke("Save", 30f);

            Destroy(this.transform.GetChild(0).gameObject, 4);
            Destroy(this, 30f);
        }
    }

}
