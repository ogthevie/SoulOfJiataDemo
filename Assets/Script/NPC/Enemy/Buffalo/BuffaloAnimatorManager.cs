using UnityEngine;
using SJ;

public class BuffaloAnimatorManager : EnemyAnimatorManager
{
    BuffaloPattern buffaloPattern;
    BuffaloManager buffaloManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        buffaloPattern = GetComponent<BuffaloPattern>();
        buffaloManager = GetComponent<BuffaloManager>();
    }

    void OnAnimatorMove()
    {
        if(!playerManager.onOption)
        {
            anim.SetInteger("distance", buffaloPattern.indexDistance);
            anim.SetInteger("attack", buffaloPattern.indexAttack);
            anim.SetBool("iStun", buffaloManager.iStun);
            anim.SetBool("dead", buffaloManager.isDead);
            anim.SetBool("hellbow", buffaloManager.isHellbow);
            anim.SetBool("canAttack", buffaloPattern.canAttack);
            anim.SetBool("isEndFight", buffaloPattern.playerManager.isDead);
            anim.SetBool("isActive", buffaloManager.isReady);
            float delta = Time.deltaTime;
            buffaloPattern.buffaloRigidbody.linearDamping = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            //if(buffaloManager.isDead)   Destroy(this);
            
            if(delta > 0f) buffaloPattern.buffaloRigidbody.linearVelocity = velocity;  
        }
    }
}
