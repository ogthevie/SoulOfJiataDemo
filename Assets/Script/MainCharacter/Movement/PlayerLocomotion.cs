using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SJ
{
    public class PlayerLocomotion : MonoBehaviour
    {
        #region Variables
        public CameraManager cameraManager;
        InputManager inputManager;
        PlayerManager playerManager;
        PlayerStats playerStats;
        AnimatorManager animatorManager;
        AudioManager audioManager;
        PlayerAttacker playerAttacker;
        PlayerUIManager playerUIManager;
        public Transform cameraObject;
        [HideInInspector] public Vector3 moveDirection;

        [HideInInspector] public Transform myTransform;


        public new Rigidbody rigidbody;
        public  GameObject normalCamera;

        [Header("Ground & Air Detection Stats")]
        [SerializeField]
        float groundDetetctionRayStartPoint = 0.5f;
        [SerializeField]
        float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        float groundDiretionRayDistance = -0.2f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;

        [Header("Movement Stats")]
        Vector3 normalVector;
        Vector3 targetPosition;
        Vector3 checkGroundPosition =  new Vector3 (0, 0.5f, 0);
        Vector3 interactbox = new (0.6f, 0.25f, 0.25f);
        public bool jumpFlag;
        
        public float speed;
        public float movementSpeed;
        [SerializeField] float walkSpeed;
        public float sprintSpeed;
        [SerializeField] float rotationSpeed = 8f;
        [SerializeField] float fallingSpeed = 800f;
        [SerializeField] GameObject originGym;
        public bool canGym, isFlipping;
        [SerializeField] Canvas loadingScreen;
        #endregion

        
        private void Awake() 
        {
            cameraManager = FindFirstObjectByType<CameraManager>();
            normalCamera = cameraManager.transform.GetChild(0).GetChild(0).gameObject;
            rigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStats>();
            playerAttacker = GetComponent<PlayerAttacker>();
            audioManager = GetComponent<AudioManager>();
            playerUIManager = FindFirstObjectByType<PlayerUIManager>();
        }
        void Start()
        {
            //cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorManager.Initialize();
            
            movementSpeed = 6f;
            walkSpeed = 2f;
            sprintSpeed = 11f;
            isFlipping = false;

            playerManager.isGrounded = true;
            ignoreForGroundCheck = ~(1 << 8);
        }

        private void HandleRotation (float delta)
        {
            if(inputManager.lockOnFlag)
            {
                walkSpeed = movementSpeed;
                if(inputManager.sprintFlag || inputManager.flipFlag)
                {
                    Vector3 targetDirection = Vector3.zero;
                    targetDirection = cameraManager.cameraTransform.forward * inputManager.vertical;
                    targetDirection += cameraManager.cameraTransform.right * inputManager.horizontal;
                    targetDirection.Normalize();
                    targetDirection.y = 0;

                    if (targetDirection == Vector3.zero)
                    {
                        targetDirection = transform.forward;
                    }

                    Quaternion tr = Quaternion.LookRotation(targetDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                    transform.rotation = targetRotation;
                }
                else
                {
                    walkSpeed = 2;
                    Vector3 rotationDirection = moveDirection;
                    rotationDirection = cameraManager.currentLockOnTarget.transform.position - transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                    transform.rotation = targetRotation;
                }
            }
            else
            {
                walkSpeed = 2f;
                Vector3 targetDir = Vector3.zero;
                float moveOverride = inputManager.moveAmount;

                targetDir = cameraObject.forward * inputManager.vertical;
                targetDir += cameraObject.right * inputManager.horizontal;

                targetDir.Normalize();
                targetDir.y = 0;

                if(targetDir == Vector3.zero)
                {
                    targetDir = myTransform.forward;

                }
                    

                float rs = rotationSpeed;

                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

                myTransform.rotation = targetRotation;
            }

        }

        public void HandleFootStep()
        {
            Debug.DrawRay(myTransform.position + new Vector3 (0f, 0.75f, 0f), -Vector3.up * 1f, Color.green, 0.85f);
            if(Physics.Raycast(myTransform.position + new Vector3 (0f, 0.75f, 0f), -Vector3.up, out RaycastHit hit, 0.8f))
            {
                if(hit.collider.gameObject.layer == 4)  audioManager.footstepSound = audioManager.footStepSfx[1];
                else if(hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 6) audioManager.footstepSound = audioManager.footStepSfx[0];
            }
        }

        public void HandleMovementAngle()
        {
            Debug.DrawRay(playerAttacker.interactOriginRay.transform.position + checkGroundPosition, playerAttacker.interactOriginRay.transform.forward * 1.5f, Color.red, 0.5f);
            if(Physics.BoxCast(playerAttacker.interactOriginRay.transform.position + checkGroundPosition, interactbox, playerAttacker.interactOriginRay.transform.forward, out RaycastHit hit, Quaternion.identity, 2.5f))
            {
                if(hit.collider.gameObject.layer == 9) 
                {
                    animatorManager.PlayTargetAnimation("DodgeBack", true);
                    rigidbody.AddForce(- playerAttacker.interactOriginRay.transform.forward * 10f, ForceMode.Impulse);
                }
                else if(hit.collider.gameObject.layer == 16)
                {
                    playerUIManager.ShowInteractionUI("Méditer");
                    if(inputManager.InteractFlag)
                    {
                        StartCoroutine(RebornFlame(hit));
                    } 
                }
                
            }
            else playerUIManager.HiddenInteractionUI();
        }

        IEnumerator RebornFlame(RaycastHit hit)
        {
            animatorManager.PlayTargetAnimation("Save", true);
            yield return new WaitForSeconds(6f);
            hit.collider.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            playerStats.TakeStaminaDamage(50);
            playerStats.AddHealth(25);
        }

        public void HandleMovement(float delta)
        {
            if(inputManager.flipFlag) return;
            
            if(playerManager.isInteracting) return;

            moveDirection = cameraObject.forward * inputManager.vertical;
            moveDirection += cameraObject.right * inputManager.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            speed = movementSpeed;

            if(inputManager.moveAmount < 0.55f)
            {
                speed = walkSpeed;
                playerManager.isSprinting = false;
            }
            if(inputManager.sprintFlag && inputManager.moveAmount > 0.8f && playerStats.currentEndurance > 0)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
            }
            else
            {
                moveDirection *= speed;
                playerManager.isSprinting = false;
            }
    
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            if(!rigidbody.isKinematic) rigidbody.linearVelocity = projectedVelocity;

            if(inputManager.lockOnFlag && inputManager.sprintFlag == false)
            {
                animatorManager.UpdateAnimatorValue(inputManager.vertical, inputManager.horizontal, playerManager.isSprinting);
            }
            else
            {
                animatorManager.UpdateAnimatorValue(inputManager.moveAmount, 0, playerManager.isSprinting);
            }

            if(animatorManager.canRotate)
            {
                HandleRotation(delta);
            }
        }
        public void HandleFlipping(float delta)
        {
            if(!canGym || playerManager.isInteracting)
                return;

            if(inputManager.flipFlag)
            {
                moveDirection = cameraObject.forward * inputManager.vertical;
                moveDirection += cameraObject.right * inputManager.horizontal;
                

                if(inputManager.moveAmount > 0)
                {
                    animatorManager.PlayTargetAnimation("Flip", true);
                    moveDirection.y = 0;
                    Quaternion flipRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = flipRotation;
                    isFlipping = true;
                }                
            }
        }

        public void DisableFlip()
        {
            isFlipping = false;
        }
        
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            if(loadingScreen.enabled) return;
            playerManager.isGrounded = false;
            RaycastHit hit;
            RaycastHit hitGym;
            Vector3 origin =  myTransform.position;
            origin.y += groundDetetctionRayStartPoint;

            if(Physics.Raycast(origin, -Vector3.up, out hit, 2f)) moveDirection = Vector3.zero;
            if(Physics.Raycast(originGym.transform.position, - Vector3.up, out hitGym, 2f)) moveDirection = Vector3.zero;


            if((hitGym.point.y - hit.point.y) > 0.25f || (hitGym.point.y - hit.point.y) < -0.25f) canGym = false;
            else canGym = true;

            if(playerManager.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir *groundDiretionRayDistance;

            targetPosition = myTransform.position;
            
            if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if(playerManager.isInAir)
                {
                    if(inAirTimer > 0.5f && inAirTimer < 1f)
                    {
                        animatorManager.PlayTargetAnimation("Landing", true);
                        inAirTimer = 0;
                    }
                    else if(inAirTimer >= 1f && inAirTimer < 5f)
                    {
                        animatorManager.PlayTargetAnimation("Hard Landing", true);
                        int landDamage = (int)Math.Truncate(inAirTimer);
                        //playerStats.TakeDamage(landDamage, 3);
                        inAirTimer = 0;
                    }
                    else if(inAirTimer >= 5f)
                    {
                        //playerStats.TakeDamage(1000, 3);
                        
                        if(playerStats.stateJiataData.isIndomitable)
                            animatorManager.PlayTargetAnimation("Hard Landing", true);

                        inAirTimer = 0;
                    }
                    else
                    {
                        animatorManager.PlayTargetAnimation("Empty", true);
                        inAirTimer = 0;
                    }

                    playerManager.isInAir = false;
                }
            }
            else
            {
                if(playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }

                Vector3 vel = rigidbody.linearVelocity;
                vel.Normalize();
                rigidbody.linearVelocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;

                if(inAirTimer > 0.2f) animatorManager.PlayTargetAnimation("Falling", true);



            }

            if(playerManager.isGrounded)
            {
                if(playerManager.isInteracting || inputManager.moveAmount > 0)
                {
                    myTransform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }

        }

        public void HandleJumping()
        {
            if(playerManager.isInteracting || !canGym || playerManager.isSprinting)
                return;

            if(inputManager.lb_input)
                return;

            if(!playerManager.onOption) return;
                
            if(inputManager.south_input)
            {
                if(inputManager.moveAmount > 0)
                {
                    if(Time.timeScale == 0) return;
                    jumpFlag = true;
                    moveDirection = cameraObject.forward * inputManager.vertical;
                    moveDirection += cameraObject.right * inputManager.horizontal;
                    animatorManager.PlayTargetAnimation("Jumping", true);
                    moveDirection.y = 0;
                    Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = jumpRotation;
                }
            }
            
        }

        public void DisableKinematic()
        {
            rigidbody.isKinematic = false;
        }
        public void DisableJumpFlag()
        {
            jumpFlag = false;
        }
    
    }
}