using UnityEngine;
using SJ;

public abstract class EventStoryTriggerManager : MonoBehaviour
{
    protected PlayerManager playerManager;
    protected InputManager inputManager;
    protected AnimatorManager animatorManager;
    protected AudioManager audioManager;
    protected GameSaveManager gameSaveManager;

    void Start()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
    }

    protected abstract void OnCollisionEnter(Collision other);
}
