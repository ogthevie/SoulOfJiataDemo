using UnityEngine;

public class SurchargeEventManager : EventStoryTriggerManager
{
    BaseDoorManager baseDoorManager;

    void Start()
    {
        baseDoorManager = FindObjectOfType<BaseDoorManager>();
        if(baseDoorManager.base_Door)
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

            Invoke("Save", 30f);

            Destroy(this.transform.GetChild(0).gameObject, 4);
            Destroy(this, 30f);
        }
    }

}
