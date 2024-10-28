using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    StoryManager storyManager;

    void Awake()
    {
        storyManager = FindFirstObjectByType<StoryManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if(transform.parent.TryGetComponent<CharacterManager>(out CharacterManager characterManager))
            {
                if(characterManager.levelStoryActions.Contains(storyManager.storyStep))storyManager.checkstoryStep(true);
            }
        }
        
    }
}
