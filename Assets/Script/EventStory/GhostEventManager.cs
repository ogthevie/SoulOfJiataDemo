using UnityEngine;

public class GhostEventManager : EventStoryTriggerManager
{
    BomboktanManager bomboktanManager;
    [SerializeField] GameObject explosionFx;
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
            explosionFx.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            animatorManager.PlayTargetAnimation("PowerUp", true);
            StartCoroutine(gameManager.StartHandleAchievement("--l'esprit des hauts plateaux--"));
            playerManager.canSomm = true;

            
            //Tutoscreen
            grotteKossiManager.enemySpawnFour.SetActive(true);

            storyManager.storyStep = 32;
            bomboktanManager.Spawn(2);
            
            Invoke("Save", 5f);

            Destroy(this.transform.GetChild(0).gameObject, 4);
            Destroy(this, 30f);
        }
    }

}
