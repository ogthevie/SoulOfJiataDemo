using UnityEngine;
using Random = UnityEngine.Random;
using SJ;

public class SteleManager : MonoBehaviour
{
    public GameObject poweFxGo, flameActivation;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] float timer;
    [SerializeField] Transform spawnPoint;
    public int state;
    public Material lightingMaterial;
    bool canTimeCheck;

    private void Start()
    {
        if(state == 1)
        {
            poweFxGo.SetActive(true);
            flameActivation.SetActive(true);
        }   
    }


    private void Update()
    {
        float delta = Time.deltaTime;
        if(canTimeCheck) timer += delta; 

    }

    private void OnTriggerEnter(Collider other)
    {
        canTimeCheck = true;        
    }

    private void OnTriggerStay(Collider other)
    {
        if(state != 1) HandleSpawnTrashMob();
    }

    void HandleSpawnTrashMob()
    {
        if(timer > 10 && transform.childCount < 6)
        {
            var trashMob = Instantiate(enemyManager.gameObject, spawnPoint.position, Quaternion.identity, null);
            trashMob.transform.SetParent(transform);
            timer = 0f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        canTimeCheck = false;
    }

}
