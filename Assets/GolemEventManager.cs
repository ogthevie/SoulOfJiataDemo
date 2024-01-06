using System.Collections;
using SJ;
using UnityEngine;

public class GolemEventManager : EventStoryTriggerManager
{
    public GameObject KossiDoor;
    [SerializeField] GameObject tololBase, smoke;
    [SerializeField] Vector3 [] tololBasePositions = new Vector3 [4];
    [SerializeField] Quaternion [] tololQuaternions = new Quaternion [4];
    [SerializeField] TololManager[] tololManagers = new TololManager [4];
    [SerializeField] bool tololExist;

    protected override void OnCollisionEnter(Collision other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && storyManager.storyStep == 21)
        {
            StartCoroutine(tutoEvent());
        }
    }

    IEnumerator tutoEvent()
    {
        playerManager.GetComponent<AudioManager>().FightTheme();
        yield return new WaitForSeconds(5f);
        HandleInstantiateTolols();
    }

    public void HandleInstantiateTolols()
    {
        if(tololExist) return;

        for(int i = 0; i < 2; i++)
        {
            Instantiate(smoke, tololBasePositions[i], tololQuaternions[i]);
            Instantiate(tololBase, tololBasePositions[i], tololQuaternions[i]);
        }
        
        tololManagers = FindObjectsOfType<TololManager>();
        tololExist = true;
    }

    void OnTriggerStay(Collider other)
    {
        if(tololExist)
        {
            if(tololManagers[1] == null && tololManagers[0] == null) 
            {
                StartCoroutine(golemAchievement());
            }

        }

        IEnumerator golemAchievement()
        {
            animatorManager.PlayTargetAnimation("Victory", true);
            storyManager.storyStep = 5;
            KossiDoor.SetActive(true);
            gameSaveManager.SaveAllData();
            StartCoroutine(playerManager.HandleAchievement("Entrainement de Libum"));
            yield return new WaitForSeconds(0.2f);
            Destroy(this, 1.5f);
        }
    }
}
