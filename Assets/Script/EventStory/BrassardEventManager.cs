using UnityEngine;

public class BrassardEventManager : EventStoryTriggerManager
{

    BomboktanManager bomboktanManager;
    void Start()
    {
        bomboktanManager = FindFirstObjectByType<BomboktanManager>();
        if(playerManager.haveGauntlet) Destroy(this.gameObject, 0.1f);
    }
    protected override void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 3 && !playerManager.haveGauntlet)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            animatorManager.PlayTargetAnimation("PowerUp", true);
            playerManager.gauntlet.SetActive(true);
            playerManager.haveGauntlet = true;
            StartCoroutine(gameManager.StartHandleAchievement("la rage du Mpodol"));
            storyManager.storyStep = 4;
            StartCoroutine(playerManager.tutoManager.HandleToggleTipsUI("Qu'est ce que ce truc ?"));
            StartCoroutine(bomboktanManager.SpawnBomboktan(4));
            Invoke("Save", 3f);
            Destroy(this.gameObject, 15f);
        }            
    }
}
