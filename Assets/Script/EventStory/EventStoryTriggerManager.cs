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
    [SerializeField]protected CameraShake cameraShake;

    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        gameManager = gameSaveManager.GetComponent<GameManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = playerManager.GetComponent<InputManager>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();
        storyManager = gameSaveManager.GetComponent<StoryManager>();
        audioManager = playerManager.GetComponent<AudioManager>();
    }

    protected void Save()
    {
        gameSaveManager.SaveAllData();
    }

    protected abstract void OnTriggerEnter(Collider other);
}
