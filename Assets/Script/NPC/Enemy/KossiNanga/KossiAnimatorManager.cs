using UnityEngine;

public class KossiAnimatorManager : EnemyAnimatorManager
{
    KossiPattern kossiPattern;

    void Start()
    {
        anim = GetComponent<Animator>();
        kossiPattern = GetComponent<KossiPattern>();
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
