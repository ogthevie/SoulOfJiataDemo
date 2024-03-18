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

    void LateUpdate()
    {
        if(storyManager.storyStep > -1) Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9) storyManager.storyStep += 1;
        storyManager.checkstoryStep(true);
        Destroy(this.gameObject, 0.1f);
    }
}
