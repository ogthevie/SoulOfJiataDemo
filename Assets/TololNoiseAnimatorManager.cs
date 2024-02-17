using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class TololNoiseAnimatorManager : EnemyAnimatorManager
{
    PlayerManager playerManager;
    TololNoisePattern tololNoisePattern;
    TololNoiseManager tololNoiseManager;
    void Awake()
    {
        anim = GetComponent<Animator>();
        tololNoisePattern = GetComponent<TololNoisePattern>();
        tololNoiseManager = GetComponent<TololNoiseManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }


    private void OnAnimatorMove()
    {
        if(!playerManager.onPause)
        {
            anim.SetFloat("distAttack", tololNoisePattern.distanceFromTarget);
            anim.SetFloat("timeAttack", tololNoisePattern.timeAttack);
            anim.SetBool("attackMode", !tololNoiseManager.isPreformingAction);
            float delta = Time.deltaTime;
            tololNoisePattern.tololNoiseRigibody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f) tololNoisePattern.tololNoiseRigibody.velocity = velocity;
        }
        else return;

    }
}
