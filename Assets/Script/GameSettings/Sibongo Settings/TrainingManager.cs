using System.Collections;
using UnityEngine;
using DG.Tweening;
using SJ;

public class TrainingManager : MonoBehaviour
{
    bool isTraining;
    [SerializeField] GameObject tolol, spawnTolol;
    [SerializeField] TutoManager tutoManager;
    [SerializeField] AudioSource umNyobeSource, trainingSource;
    [SerializeField] GameManager gameManager;

    private void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        tutoManager = GameObject.Find("Tuto").GetComponent<TutoManager>();
        umNyobeSource = GameObject.Find("StatutUmNyobe").GetComponent<AudioSource>();
        trainingSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(!isTraining) return;
        if(tolol == null) LoadEnemy(other.gameObject);
   
    }

    void OnTriggerEnter(Collider other)
    {
        if(isTraining) return;

        if(other.gameObject.layer == 3)
        {
            StartCoroutine(EnableTraining(other.gameObject));
            umNyobeSource.enabled = false;
            trainingSource.enabled = true;
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<TololManager>(out TololManager component))
        {
            float distanceTololRuneDome = Vector3.Distance(transform.position, other.transform.position);
            component.TakeDamage(1000);
        }
        if(other.gameObject.layer == 3)
        {
            tutoManager.HiddenUI();
            if(FindObjectOfType<DayNightCycleManager>().dayTimer < 3) umNyobeSource.enabled = true;
            isTraining = false;
            trainingSource.enabled = false;
        }
    }

    IEnumerator EnableTraining(GameObject player)
    {
        StartCoroutine(gameManager.ZoneEntry("Cercle des Prodiges", "Lituba"));
        tutoManager.ShowUI();
        tutoManager.tipsUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        LoadEnemy(player);
        isTraining = true;
    }

    private void LoadEnemy(GameObject player)
    {
        Vector3 tololPosition = transform.position + new Vector3 (-10f, 0f, -10f);
        var visual = Instantiate(spawnTolol, tololPosition, Quaternion.identity);
        tolol = visual;
    }
}
