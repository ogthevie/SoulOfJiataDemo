using System.Collections;
using SJ;
using UnityEngine;

public class GolemEventManager : EventStoryTriggerManager
{

    [SerializeField] Collider golemCollider, forestPortalCollider;
    [SerializeField] Material activateMaterial;
    [SerializeField] GameObject mbuuGhost;

    void Start()
    {
        mbuuGhost.SetActive(false);
        cameraShake = FindFirstObjectByType<CameraShake>();
        if(storyManager.storyStep < 6) golemCollider.enabled = false;

        if(storyManager.storyStep > 6)
        {
            HandlePortal();
            Destroy(this, 0.5f);
        }
    }
    private void LateUpdate()
    {
        HandleMbuuSpawn();
    }

    public IEnumerator BridgeEvent()
    {
        yield return new WaitForSeconds (0.5f);
        storyManager.storyStep = 7;
        cameraShake.Shake(7, 0.15f);
        yield return new WaitForSeconds(5f);
        StartCoroutine(gameManager.StartHandleAchievement("L'éveil de l'HOMME DANS LA PIERRE"));
        yield return new WaitForSeconds(3.5f);
        HandlePortal();
        //gameSaveManager.SaveAllData();
    }

    private void HandlePortal()
    {
        forestPortalCollider.enabled = false;
        forestPortalCollider.transform.GetChild(0).GetComponent<Renderer>().material = activateMaterial;
        var materials = forestPortalCollider.transform.GetChild(1).GetComponent<Renderer>().materials;
        materials[1] = activateMaterial;
        forestPortalCollider.transform.GetChild(1).GetComponent<Renderer>().materials = materials;
        if(this != null) Destroy(this);
    }

    protected override void OnTriggerEnter(Collider other)
    {

    }

    void HandleMbuuSpawn()
    {
        if(storyManager.storyStep == 2)
        {
            float distance = Vector3.Distance(this.transform.position, playerManager.transform.position);
            if(distance <= 620) mbuuGhost.SetActive(true);
        }

    }
}
