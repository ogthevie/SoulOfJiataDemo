using UnityEngine;


public class KeliperAnimatorManager : EnemyAnimatorManager
{
    [SerializeField] KeliperPattern keliperPattern;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove() 
    {
        if(!playerManager.onOption)
        {
            anim.SetFloat("distAttack", keliperPattern.distanceFromTarget);
            //anim.SetFloat("timeAttack", tololPattern.timeAttack);
            anim.SetBool("bulletAttack", keliperPattern.bulletAttack);
            anim.SetBool("stunt", keliperPattern.stunt);
            anim.SetBool("isHit", keliperPattern.isHit);
            float delta = Time.deltaTime;
            keliperPattern.keliperRigibody.linearDamping = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f && !keliperPattern.keliperRigibody.isKinematic) keliperPattern.keliperRigibody.linearVelocity = velocity;            
        }    
    }
}
