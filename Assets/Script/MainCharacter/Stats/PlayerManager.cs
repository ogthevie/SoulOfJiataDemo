using System.Collections;
using UnityEngine;


namespace SJ
{
    public class PlayerManager : MonoBehaviour
    {
        DeveloperModeManager developerModeManager;
        
        InputManager inputManager;
        Animator anim;
        AnimatorManager animatorManager;
        PlayerLocomotion playerLocomotion;
        CameraManager cameraManager;
        PlayerStats playerStats;
        PlayerAttacker playerAttacker;
        AudioManager audioManager;
        public SkillTreeManager skillTreeManager;

        public GameObject magnetiFX;
        public GameObject pauseMenu;

        public GameObject dialogUI;
        public GameObject brassardG, brassardL, brasG, brasL, mask, maskEye;

        public Transform lockOnTransform;
        public bool isDead;
        public bool takeDamage;
        public bool isInteracting;

        [Header("Player flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canAttack;
        public bool canSurcharge, canArcLight, canThunder;
        public bool canDoCombo;
        public bool onPause;
        public bool onTutoScreen;
        public bool canPass;
        public bool onInventory;
        public bool onSorceryTree;

        public static bool created = false;


        private void Awake()
        { 
            developerModeManager = GetComponent<DeveloperModeManager>();
            cameraManager = FindObjectOfType<CameraManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            skillTreeManager = FindObjectOfType<SkillTreeManager>();
            audioManager = GetComponent<AudioManager>();
            pauseMenu = GameObject.Find("Pause");
            dialogUI = GameObject.Find("Dialog");
 
        }

        public void Start() 
        {
            inputManager = GetComponent<InputManager>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            isDead = false;
            playerStats = GetComponent<PlayerStats>();
            skillTreeManager = GameObject.Find("SkillTree").GetComponent<SkillTreeManager>();
            pauseMenu.SetActive(false);
            dialogUI.SetActive(false);
            onPause = false;
            
            HandleSurchargeBrassard();
            //mask.GetComponent<SkinnedMeshRenderer>().enabled = false;
            //maskEye.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }

        public void Update()
        {
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            anim.SetBool("isInAir", isInAir);

            cameraManager.DestroyLockOntargets();

            float delta = Time.deltaTime;

            HandleLife();
            
            if(isDead)
                return;
            inputManager.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleFlipping(delta);
            playerLocomotion.HandleJumping();
            playerLocomotion.HandleFootStep();
            playerStats.HandleReloadStamina(delta);
            playerStats.HandleEndurance();
            HandleInventory();
            playerAttacker.HandleSorceryPad();


            developerModeManager.HandleInstantiateVases();
            developerModeManager.HandleInstantiateTolols();
            developerModeManager.HandleStats();
            developerModeManager.LoadSave();
            developerModeManager.ResetSave();
            developerModeManager.HandleInstantiateKossi();
            developerModeManager.HandleInstantiateKossiKaze();

            //HandleTutoScreen();

            
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if(isDead) return;
            if(cameraManager != null)
            {
                cameraManager.FollowTarget();
                cameraManager.HandleCameraRotation(delta, inputManager.mouseX, inputManager.mouseY);
            }
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);

            //playerAttacker.HandleInteractTree();
            playerLocomotion.HandleMovementAngle();
            
        }

        private void LateUpdate()
        {
            isSprinting = inputManager.lt_input;
            inputManager.rt_input = false;
            inputManager.lowAttack_input = false;
            inputManager.highAttack_input = false;
            inputManager.north_input = false;
            inputManager.west_input = false;
            inputManager.south_input = false;
            inputManager.magicFlag = false;

            inputManager.left_input = false;
            inputManager.right_input = false;
            inputManager.up_input = false;
            inputManager.down_input = false;

            inputManager.start_input = false;
            inputManager.left_menu_input = false;
            inputManager.right_menu_input = false;


            ///////////////// DEV MODE MODULES /////////////////////////////////
            inputManager.one_input = false;
            inputManager.two_input = false;
            inputManager.three_input = false;
            inputManager.four_input = false;
            inputManager.five_input = false;
            inputManager.six_input = false;
            inputManager.seven_input = false;
            ///////////////////////////////////////////////////////////////////



            if(isInAir)
            {
                canAttack = false;
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
            else
            {
                canAttack = true;
            }
            skillTreeManager.HandleActivateSlot();

        }

        public void EnablemagnetiFX()
        {
            magnetiFX.SetActive(true);
            GameObject magnetiFXClone = Instantiate(magnetiFX, transform);
            Destroy(magnetiFXClone, 1.5f);
        }

        public void HandleInventory()
        {
            if(onPause && !isInteracting)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        public void HandleTutoScreen()
        {
            if(onTutoScreen && !isInteracting)
            {
                Time.timeScale = 0f;
            }
            else if(!onTutoScreen)
            {
                Time.timeScale = 1f;
            }
        }
        public void HandleBoolTakeDamage()
        {
            if(takeDamage)
            {
                takeDamage = false;
                playerAttacker.DisableArcLightningFx();
            } 

        }

        public void HandleLife()
        {
            if(isDead)
            {
                if(Input.GetKey(KeyCode.End))
                {
                    animatorManager.PlayTargetAnimation("Wake", true);
                    playerStats.AddHealth(playerStats.maxHealth/2);
                    isDead = false;
                    StartCoroutine(TempIndomitable());
                }

                IEnumerator TempIndomitable()
                {
                    playerAttacker.statesJiataData.isHidden = true;
                    yield return new WaitForSeconds(8f);
                    playerAttacker.statesJiataData.isHidden = false;
                }              
            }
            else
            {
                if(Input.GetKey(KeyCode.M))
                {
                    playerStats.TakeDamage(1000, 3);
                }
            }
            
        }

        public void HandleSurchargeBrassard()
        {
            if(canSurcharge)
                {
                    brassardL.GetComponent<SkinnedMeshRenderer>().enabled = true;
                    brasL.GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
            else
                {
                    brassardL.GetComponent<SkinnedMeshRenderer>().enabled = false;
                    brasL.GetComponent<SkinnedMeshRenderer>().enabled = false;
                }
        }
    }
}