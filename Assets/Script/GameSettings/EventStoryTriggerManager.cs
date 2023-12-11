using UnityEngine;
using SJ;

public abstract class EventStoryTriggerManager : MonoBehaviour
{
    protected PlayerManager playerManager;
    protected InputManager inputManager;
    protected AnimatorManager animatorManager;
    protected GameSaveManager gameSaveManager;
    protected GrotteKossiManager grotteKossiManager;
    protected StoryManager storyManager;
    protected CameraShake cameraShake;

    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();
        grotteKossiManager = FindObjectOfType<GrotteKossiManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        storyManager = gameSaveManager.GetComponent<StoryManager>();
    }

    protected void Save()
    {
        gameSaveManager.SaveAllData();
    }

    protected abstract void OnCollisionEnter(Collision other);
}
