using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class KossiKazeAnimatorManager : EnemyAnimatorManager
{
    PlayerManager playerManager;
    kossiKazePattern kossiKazePattern;

    void Awake()
    {
        anim = GetComponent<Animator>();
        kossiKazePattern = GetComponent<kossiKazePattern>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnAnimatorMove() 
    {
        if(!playerManager.onPause)
        {
            anim.SetFloat("distAttack", kossiKazePattern.distanceFromTarget);
            //anim.SetFloat("timeAttack", tololPattern.timeAttack);
            anim.SetBool("canExplose", kossiKazePattern.canExplose);
            float delta = Time.deltaTime;
            kossiKazePattern.kossiKazeRigibody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f) kossiKazePattern.kossiKazeRigibody.velocity = velocity;            
        }    
    }
}
