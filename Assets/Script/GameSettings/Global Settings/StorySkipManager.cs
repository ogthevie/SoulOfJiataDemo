using UnityEngine;

public class StorySkipManager : MonoBehaviour
{
    StoryManager storyManager;
    TutoManager tutoManager;

    void Start()
    {
        storyManager = FindFirstObjectByType<StoryManager>();
        if(storyManager.storyStep > -1) Destroy(this.gameObject);
        tutoManager = FindFirstObjectByType<TutoManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && storyManager.storyStep == -1 )
        {
            storyManager.checkstoryStep(true);
            GameManager gameManager = storyManager.GetComponent<GameManager>();
            StartCoroutine(gameManager.ZoneEntry("...Sibongo...", "Lituba"));
            StartCoroutine(tutoManager.HandleToggleTipsUI("Si je veux m'intégrer, je dois discuter avec les autres"));
            tutoManager.dialogTuto = true;
            Destroy(this.gameObject, 9f);
        }

    }
}
