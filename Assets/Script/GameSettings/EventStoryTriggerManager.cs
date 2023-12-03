using UnityEngine;
using SJ;

public abstract class EventStoryTriggerManager : MonoBehaviour
{
    protected PlayerManager playerManager;
    protected InputManager inputManager;
    protected AnimatorManager animatorManager;
    protected GameSaveManager gameSaveManager;
    protected GrotteKossiManager grotteKossiManager;

    void Awake()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
        grotteKossiManager = FindObjectOfType<GrotteKossiManager>();
    }

    protected void Save()
    {
        gameSaveManager.SaveAllData();
    }

    protected abstract void OnCollisionEnter(Collision other);
}
