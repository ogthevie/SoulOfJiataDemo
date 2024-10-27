using UnityEngine;

public class SpiritEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindFirstObjectByType<BomboktanManager>();

        if(storyManager.storyStep >= 6)
        {
             Destroy(this.transform.GetChild(0).gameObject);
             Destroy(this);
        } 
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && storyManager.storyStep < 6)
        {
            animatorManager.PlayTargetAnimation("PowerUp", true);
            StartCoroutine(gameManager.StartHandleAchievement("L'ESPRIT DE LA PIERRE"));
            
            storyManager.storyStep = 6;
            StartCoroutine(bomboktanManager.SpawnBomboktan(3));
            
            Invoke("Save", 5f);

            StartCoroutine(gameManager.StartHandleToDo(storyManager.storyStep));

            this.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }
    

}
