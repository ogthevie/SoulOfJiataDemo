using UnityEngine;

public class SurchargeEventManager : EventStoryTriggerManager
{
    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);

            playerManager.brasL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            playerManager.brassardL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            //Tutoscreen
            playerManager.canSurcharge = true;

            gameSaveManager.SaveAllData();

            Destroy(this.transform.GetChild(0).gameObject, 4);
            Destroy(this, 5f);
        }

        
    }
}
