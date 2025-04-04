using UnityEngine;

namespace SJ
{
    public class AnimatorManager : MonoBehaviour
    {
        #region  Variables
        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;
        
        public bool animationState;


        #endregion

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            playerManager = GetComponentInParent<PlayerManager>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
            anim.SetFloat("BoostBaemb", 1f);
            canRotate = true;
            animationState = true;
        }
        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void UpdateAnimatorValue(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region vertical
            float v = 0;

            if(verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if(verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if(verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if( verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region horizontal
            float h = 0;

            if(horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if(horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if(horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if(isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }
        private void OnAnimatorMove()
         {
            if(playerManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.linearDamping = 0;
            Vector3 deltaPosition =  anim.deltaPosition;
            deltaPosition.y = 0;
            if(Time.timeScale != 0)
            {
                Vector3 velocity =  deltaPosition / delta;
                playerLocomotion.rigidbody.linearVelocity = velocity;            
            }
         }
    }
}
