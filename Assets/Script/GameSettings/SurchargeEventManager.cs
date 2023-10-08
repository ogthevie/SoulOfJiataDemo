using UnityEngine;

public class SurchargeEventManager : EventStoryTriggerManager
{
    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3)
        {
            playerManager.brasL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            playerManager.brassardL.GetComponent<SkinnedMeshRenderer>().enabled = true;
            //Tutoscreen
            inputManager.jiatastats.canSurcharge = true;
            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this);
        }

        
    }
}
