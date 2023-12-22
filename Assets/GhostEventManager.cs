using UnityEngine;

public class GhostEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindObjectOfType<BomboktanManager>();

        if(playerManager.canSomm)
        {
            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3 && !playerManager.canSomm)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canSomm = true;

            
            //Tutoscreen
            grotteKossiManager.enemySpawnFour.SetActive(true);
            
            storyManager.storyStep = 53;
            bomboktanManager.Spawn(2);
            
            Invoke("Save", 30f);

            Destroy(this.transform.GetChild(0).gameObject, 4);
            Destroy(this, 30f);
        }
    }

}
