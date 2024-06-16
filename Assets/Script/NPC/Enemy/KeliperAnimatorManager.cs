using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class KeliperAnimatorManager : EnemyAnimatorManager
{
    PlayerManager playerManager;
    KeliperPattern keliperPattern;
    KeliperManager keliperManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        keliperPattern = GetComponent<KeliperPattern>();
        keliperManager = GetComponent<KeliperManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnAnimatorMove() 
    {
        if(!playerManager.onOption)
        {
            anim.SetFloat("distAttack", keliperPattern.distanceFromTarget);
            //anim.SetFloat("timeAttack", tololPattern.timeAttack);
            anim.SetBool("bulletAttack", keliperPattern.bulletAttack);
            anim.SetBool("stunt", keliperPattern.stunt);
            anim.SetBool("breakPoint", keliperManager.isbreak);
            float delta = Time.deltaTime;
            keliperPattern.keliperRigibody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f && !keliperPattern.keliperRigibody.isKinematic) keliperPattern.keliperRigibody.velocity = velocity;            
        }    
    }
}
