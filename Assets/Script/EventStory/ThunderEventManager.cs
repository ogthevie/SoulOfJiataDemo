using System.Linq;
using System.Collections;
using UnityEngine;
using SJ;

public class ThunderEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindObjectOfType<BomboktanManager>();
        if(playerManager.canThunder) Destroy(this.gameObject, 0.5f);
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 3 && !playerManager.canThunder)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.canThunder = true;
            
            StartCoroutine(gameManager.StartHandleAchievement("LE CRI DU CIEL"));
            GetComponent<ParticleSystem>().Stop();
            Invoke("Save", 3f);
            Destroy(this.gameObject, 5f);
        }            
    }

}
