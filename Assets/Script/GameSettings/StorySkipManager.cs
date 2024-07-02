using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySkipManager : MonoBehaviour
{
    StoryManager storyManager;

    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        if(storyManager.storyStep > -1) Destroy(this.gameObject); 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) if(storyManager.storyStep == -1 ) storyManager.checkstoryStep(true);
        GameManager gameManager = storyManager.GetComponent<GameManager>();
        StartCoroutine(gameManager.StartHandleToDo(0));
        Destroy(this.gameObject, 4.8f);
    }
}
