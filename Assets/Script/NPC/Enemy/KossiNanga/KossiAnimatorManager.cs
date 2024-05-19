using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class KossiAnimatorManager : EnemyAnimatorManager
{
    PlayerManager playerManager;
    KossiPattern kossiPattern;

    void Awake()
    {
        anim = GetComponent<Animator>();
        kossiPattern = GetComponent<KossiPattern>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnAnimatorMove() 
    {
        if(!playerManager.onOption)
        {
            anim.SetFloat("distAttack", kossiPattern.distanceFromTarget);
            //anim.SetFloat("timeAttack", tololPattern.timeAttack);
            anim.SetBool("bulletAttack", kossiPattern.bulletAttack);
            float delta = Time.deltaTime;
            kossiPattern.kossiRigibody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f) kossiPattern.kossiRigibody.velocity = velocity;            
        }    
    }
}
