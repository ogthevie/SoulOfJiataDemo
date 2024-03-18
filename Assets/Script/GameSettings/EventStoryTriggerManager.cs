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
    [SerializeField]protected CameraShake cameraShake;
    protected NotificationQuestManager notificationQuestManager;

    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();
        grotteKossiManager = FindObjectOfType<GrotteKossiManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        storyManager = gameSaveManager.GetComponent<StoryManager>();
        notificationQuestManager = FindObjectOfType<NotificationQuestManager>();
        audioManager = playerManager.GetComponent<AudioManager>();
    }

    protected void Save()
    {
        gameSaveManager.SaveAllData();
    }

    protected abstract void OnCollisionEnter(Collision other);
}
