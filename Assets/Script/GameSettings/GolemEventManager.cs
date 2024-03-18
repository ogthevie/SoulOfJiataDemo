using System.Collections;
using SJ;
using UnityEngine;

public class GolemEventManager : EventStoryTriggerManager
{
    public GameObject KossiDoor;
    [SerializeField] BoxCollider golemBoxCollider;
    //[SerializeField] GameObject tololBase, smoke;
    //[SerializeField] Vector3 tololBasePosition = new Vector3 (183.38f, 5.697f, 98.65f);
    //[SerializeField] Quaternion tololQuaternion = Quaternion.Euler(0f, 44.839f, 0f);

    //[SerializeField] TololManager tololManager;
    //[SerializeField] bool tololExist;

    void Start()
    {
        if(storyManager.storyStep > 2 )KossiDoor.SetActive(true);
    }

    /*void Update()
    {
        if(tololExist)
        {
            if(tololManager == null) 
            {
                StartCoroutine(golemAchievement());
            }
        }        
    }*/

    protected override void OnCollisionEnter(Collision other)
    {

    }

    /*public IEnumerator tutoEvent()
    {
        audioManager.FightTheme();
        golemBoxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        HandleInstantiateTolols();
    }*/

    /*public void HandleInstantiateTolols()
    {
        if(tololExist) return;

        Instantiate(smoke, tololBasePosition, tololQuaternion);
        Instantiate(tololBase, tololBasePosition, tololQuaternion);
        
        tololManager = FindObjectOfType<TololManager>();
        tololExist = true;
    }*/
    public IEnumerator golemAchievement()
    {
        //storyManager.storyStep = 4;
        KossiDoor.SetActive(true);
        //playerManager.haveMask = true;
        //playerManager.HandleMask();
        storyManager.storyStep = 3;
        gameSaveManager.SaveAllData();
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(notificationQuestManager.StartHandleAchievement("L'HOMME DANS LA PIERRE"));
        Destroy(this, 1.5f);
        golemBoxCollider.enabled = true;
    }

    public void ActivateGolemBoxCollider()
    {
        golemBoxCollider.enabled = true;
    }
}
