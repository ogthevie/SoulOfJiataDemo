using UnityEngine;
using SJ;

public class EnemyAnimatorManager : MonoBehaviour
{
    public Animator anim;
    protected PlayerAttacker playerAttacker;
    public PlayerManager playerManager;

    protected void Awake()
    {
        playerAttacker = FindFirstObjectByType<PlayerAttacker>();
        playerManager = FindFirstObjectByType<PlayerManager>();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.CrossFade(targetAnim, 0.2f);
    }
}
