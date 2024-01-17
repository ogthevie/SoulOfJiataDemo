using UnityEngine;
using SJ;
using System.Collections;

public abstract class EventStoryTriggerManager : MonoBehaviour
{
    protected PlayerManager playerManager;
    protected InputManager inputManager;
    protected AnimatorManager animatorManager;
    protected AudioManager audioManager;
    protected GameSaveManager gameSaveManager;
    protected GrotteKossiManager grotteKossiManager;
    protected StoryManager storyManager;
    protected CameraShake cameraShake;
    protected PlayerUIManager playerUIManager;

    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();
        grotteKossiManager = FindObjectOfType<GrotteKossiManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        storyManager = gameSaveManager.GetComponent<StoryManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        audioManager = playerManager.GetComponent<AudioManager>();
    }

    protected void Save()
    {
        gameSaveManager.SaveAllData();
    }

    protected abstract void OnCollisionEnter(Collision other);
}
