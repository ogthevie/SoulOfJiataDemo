using System.Collections;
using SJ;
using UnityEngine;

public class GolemEventManager : EventStoryTriggerManager
{

    [SerializeField] Collider golemCollider;
    [SerializeField] GameObject nyooRoot;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        if(storyManager.storyStep < 6) golemCollider.enabled = false;

        if(storyManager.storyStep > 6)
        {
            nyooRoot.SetActive(true);
        }

        if(storyManager.storyStep > 6) Destroy(this, 0.5f);
    }

    public IEnumerator BridgeEvent()
    {
        yield return new WaitForSeconds (0.5f);
        storyManager.storyStep = 7;
        cameraShake.Shake(7, 0.15f);
        nyooRoot.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartCoroutine(gameManager.StartHandleAchievement("L'EVEIL DE L'HOMME DANS LA PIERRE"));
        yield return new WaitForSeconds(3.5f);
        //gameSaveManager.SaveAllData();
    }

    protected override void OnTriggerEnter(Collider other)
    {

    }
}
