using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class TololAnimatorManager : EnemyAnimatorManager
{
    PlayerManager playerManager;
    TololPattern tololPattern;
    TololManager tololManager;
    void Awake()
    {
        anim = GetComponent<Animator>();
        tololPattern = GetComponent<TololPattern>();
        tololManager = GetComponent<TololManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }


    private void OnAnimatorMove()
    {
        if(!playerManager.onOption)
        {
            anim.SetFloat("distAttack", tololPattern.distanceFromTarget);
            anim.SetFloat("timeAttack", tololPattern.timeAttack);
            anim.SetBool("attackMode", !tololManager.isPreformingAction);
            float delta = Time.deltaTime;
            tololPattern.tololRigibody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f) tololPattern.tololRigibody.velocity = velocity;
        }
        else return;

    }
}
