using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySkipManager : MonoBehaviour
{
    StoryManager storyManager;
    GameManager gameManager;

    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        gameManager = storyManager.GetComponent<GameManager>();
        if(storyManager.storyStep > -1) Destroy(this.gameObject); 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if(storyManager.storyStep == -1 ) storyManager.checkstoryStep(true);
            StartCoroutine(gameManager.ZoneEntry("...SIBONGO..."));
        }  
        Destroy(this.gameObject, 4.8f);
    }
}
