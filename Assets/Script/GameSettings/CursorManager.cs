using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    StoryManager storyManager;

    void Awake()
    {
        storyManager = FindObjectOfType<StoryManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if(transform.parent.TryGetComponent<CharacterManager>(out CharacterManager characterManager))
            {
                if(characterManager.levelStoryActions.Contains(storyManager.storyStep))storyManager.checkstoryStep(true);
            }
            
            /*if(transform.parent.TryGetComponent<GolemEventManager>(out GolemEventManager component) && storyManager.storyStep == 2) 
            {
                component.ActivateGolemBoxCollider();
            }
            this.gameObject.SetActive(false);*/
        }
        
    }
}
