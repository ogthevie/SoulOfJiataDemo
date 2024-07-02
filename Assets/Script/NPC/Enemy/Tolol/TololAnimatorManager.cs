using UnityEngine;

public class TololAnimatorManager : EnemyAnimatorManager
{
    TololPattern tololPattern;
    TololManager tololManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        tololPattern = GetComponent<TololPattern>();
        tololManager = GetComponent<TololManager>();
    }


    private void OnAnimatorMove()
    {
        if(!playerManager.onOption && tololPattern != null)
        {
            anim.SetFloat("distAttack", tololPattern.distanceFromTarget);
            anim.SetFloat("timeAttack", tololPattern.timeAttack);
            anim.SetBool("attackMode", !tololManager.isPreformingAction);
            anim.SetBool("isHit", playerAttacker.isHit);
            float delta = Time.deltaTime;
            tololPattern.tololRigibody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f && !tololPattern.tololRigibody.isKinematic ) tololPattern.tololRigibody.velocity = velocity;
        }

    }
}
