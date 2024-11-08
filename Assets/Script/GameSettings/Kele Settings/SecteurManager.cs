using System.Collections;
using UnityEngine;

public class SecteurManager : MonoBehaviour
{
    [SerializeField] int indexSecteur;
    [SerializeField] StoryManager storyManager;
    [SerializeField] TutoManager tutoManager;
    [SerializeField] GameObject [] wallSecteurs;
    [SerializeField] EnemySpawnManager enemySpawnManager;
    

    private void Awake()
    {
        storyManager = FindFirstObjectByType<StoryManager>();
        tutoManager = FindFirstObjectByType<TutoManager>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(indexSecteur == 0) HandleOpeningZeroSecteur();    
    }

    private void OnTriggerStay(Collider other)
    {
        HandleClosingZeroSecteur();    
    }

    private void HandleOpeningZeroSecteur()
    {
        if(storyManager.storyStep == 2 && !wallSecteurs[0].activeSelf) StartCoroutine(OnZeroSecteur());
    }

    IEnumerator OnZeroSecteur()
    {
        enemySpawnManager.LoadEnemy();
        yield return new WaitForSeconds(0.1f);
        foreach(var wallSecteur in wallSecteurs) wallSecteur.SetActive(true);
        StartCoroutine(tutoManager.HandleToggleTipsUI("C'est à ça que ressemble une créature du Kao ? on dirait un être humain"));
    }

    private void HandleClosingZeroSecteur()
    {
        int child = enemySpawnManager.transform.childCount;
        
        if(storyManager.storyStep > 2 || child < 1)
        {
            foreach(var wallSecteur in wallSecteurs) wallSecteur.SetActive(false);
            Destroy(enemySpawnManager.gameObject);
            Destroy (this);
        }
    }
}
