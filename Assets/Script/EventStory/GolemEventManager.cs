using System.Collections;
using SJ;
using UnityEngine;

public class GolemEventManager : EventStoryTriggerManager
{
    public GameObject [] lempoyBridge = new GameObject [13];
    [SerializeField] Collider golemCollider, bridgeCollider;

    void Start()
    {
        if(storyManager.storyStep < 6) golemCollider.enabled = false;

        if(storyManager.storyStep > 6)
        {
            bridgeCollider.enabled = true;
            foreach (var bridgePart in lempoyBridge)
            {
                bridgePart.SetActive(true);
            }            
        }

        if(storyManager.storyStep > 6) Destroy(this, 0.5f);
    }

    public IEnumerator BridgeEvent()
    {
        yield return new WaitForSeconds (0.5f);
        storyManager.storyStep = 7;
        cameraShake.Shake(7, 0.15f);
        foreach (var bridgePart in lempoyBridge)
        {
            bridgePart.SetActive(true);
        }
        bridgeCollider.enabled = true;
        yield return new WaitForSeconds(5f);
        StartCoroutine(gameManager.StartHandleAchievement("--L'EVEIL DE L'HOMME DANS LA PIERRE--"));
        yield return new WaitForSeconds(3.5f);
        //gameSaveManager.SaveAllData();
    }

    protected override void OnTriggerEnter(Collider other)
    {

    }
}
