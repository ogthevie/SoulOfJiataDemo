using UnityEngine;
using SJ;
using System.Collections;

public abstract class EventStoryTriggerManager : MonoBehaviour
{
    [SerializeField] protected PlayerManager playerManager;
    protected InputManager inputManager;
    protected AnimatorManager animatorManager;
    protected AudioManager audioManager;
    protected GameSaveManager gameSaveManager;
    protected GameManager gameManager;
    protected StoryManager storyManager;
    protected TutoManager tutoManager;
    [SerializeField]protected CameraShake cameraShake;

    void Awake()
    {
        gameSaveManager = FindFirstObjectByType<GameSaveManager>();
        gameManager = gameSaveManager.GetComponent<GameManager>();
        playerManager = FindFirstObjectByType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();
        storyManager = gameSaveManager.GetComponent<StoryManager>();
        audioManager = playerManager.GetComponent<AudioManager>();
        tutoManager = FindFirstObjectByType<TutoManager>();
    }

    protected void Save()
    {
        gameSaveManager.SaveAllData();
    }

    protected abstract void OnTriggerEnter(Collider other);
}
