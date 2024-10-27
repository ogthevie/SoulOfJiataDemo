using UnityEngine;

public class KossiKazeAnimatorManager : EnemyAnimatorManager
{
    kossiKazePattern kossiKazePattern;

    void Start()
    {
        anim = GetComponent<Animator>();
        kossiKazePattern = GetComponent<kossiKazePattern>();
    }

    private void OnAnimatorMove() 
    {
        if(!playerManager.onOption && kossiKazePattern != null)
        {
            anim.SetFloat("distAttack", kossiKazePattern.distanceFromTarget);
            anim.SetBool("canExplose", kossiKazePattern.canExplose);
            float delta = Time.deltaTime;
            kossiKazePattern.kossiKazeRigibody.linearDamping = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if(delta > 0f) kossiKazePattern.kossiKazeRigibody.linearVelocity = velocity;            
        }    
    }
}
