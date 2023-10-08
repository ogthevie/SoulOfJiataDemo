using UnityEngine;
using SJ;

public abstract class EventStoryTriggerManager : MonoBehaviour
{
    protected PlayerManager playerManager;
    protected InputManager inputManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        inputManager = FindObjectOfType<InputManager>();
    }

    protected abstract void OnCollisionEnter(Collision other);
}
