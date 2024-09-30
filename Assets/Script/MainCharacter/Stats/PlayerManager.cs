using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SJ
{
    public class PlayerManager : MonoBehaviour
    {
        public TutoManager tutoManager;
        DeveloperModeManager developerModeManager;
        InputManager inputManager;
        Animator anim;
        AnimatorManager animatorManager;
        public PlayerLocomotion playerLocomotion;
        CameraManager cameraManager;
        public PlayerStats playerStats;
        PlayerAttacker playerAttacker;
        public SkillTreeManager skillTreeManager;

        //public GameObject magnetiFX;
        public GameObject optionMenu;

        public GameObject dialogUI;
        public GameObject gauntlet, transitionScreen;

        [SerializeField] GameObject deadUI;
        public Transform lockOnTransform, interactionUI;
        public bool isDead, takeDamage, isInteracting;

        [Header("Player flags")]
        public bool isSprinting, isInAir, isGrounded, canAttack;
        public bool haveGauntlet, canSurcharge, canArcLight, canThunder;
        public bool canBaemb, canSomm;
        public bool canDoCombo;
        public bool onOption, onTutoScreen, canPass;
        [SerializeField] Camera cam;


        private void Awake()
        { 
            developerModeManager = GetComponent<DeveloperModeManager>();
            cameraManager = FindObjectOfType<CameraManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            skillTreeManager = FindObjectOfType<SkillTreeManager>();
            tutoManager = FindObjectOfType<TutoManager>();
        }

        public void Start() 
        {
            inputManager = GetComponent<InputManager>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            isDead = false;
            playerStats = GetComponent<PlayerStats>();
            optionMenu.SetActive(false);
            dialogUI.SetActive(false);
            
            
            gauntlet.SetActive(haveGauntlet);
        }

        public void Update()
        {
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            anim.SetBool("isInAir", isInAir);

            cameraManager.DestroyLockOntargets();

            float delta = Time.deltaTime;
            inputManager.HandleInteractInput();

            HandleLife();
            if(!animatorManager.animationState || isDead)
                return;

            inputManager.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleFlipping(delta);
            playerLocomotion.HandleJumping();
            playerLocomotion.HandleFootStep();
            playerStats.HandleReloadStamina(delta);
            playerStats.HandleEndurance();
            HandleOptionMenu();
            playerAttacker.HandleSorceryPad();

#if UNITY_EDITOR
            developerModeManager.HandleInstantiateVases();
            developerModeManager.HandleInstantiateMobs();
            developerModeManager.HandleStats();
            developerModeManager.LoadSave();
            developerModeManager.ResetSave();
            developerModeManager.DancePlayer();
            //developerModeManager.HandleInstantiateKossiKaze();
#endif

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
            //HandleCanvasRuben();
            
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

            inputManager.left_input = false;
            inputManager.right_input = false;
            inputManager.up_input = false;
            inputManager.down_input = false;

            inputManager.start_input = false;
            inputManager.select_input = false;
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

        /*public void EnablemagnetiFX()
        {
            magnetiFX.SetActive(true);
            GameObject magnetiFXClone = Instantiate(magnetiFX, transform);
            Destroy(magnetiFXClone, 1.5f);
        }*/

        public void HandleOptionMenu()
        {
            if(onOption && !isInteracting)
            {
                Time.timeScale = 0;
                optionMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                optionMenu.SetActive(false);                
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


        public void HandleDeadUI()
        {
            StartCoroutine(DeadUIActivation());
        }
        IEnumerator DeadUIActivation()
        {
            yield return new WaitForSeconds (1.5f);
            deadUI.SetActive(true);
        }

        IEnumerator HandleSaveByPlayer()
        {
            transitionScreen.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(0.8f);
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                FindObjectOfType<DayNightCycleManager>().dayTimer = Random.Range(0,4);
                SibongoManager sibongoManager = FindObjectOfType<SibongoManager>();
                sibongoManager.HandleDayPeriod();
            }

            CharacterManager [] characterManagers = FindObjectsOfType<CharacterManager>();
            foreach (var elt in characterManagers)
            {
                elt.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds (1f);

            foreach (var elt in characterManagers)
            {
                elt.gameObject.SetActive(true);
            }
            FindObjectOfType<GameSaveManager>().SaveAllData();
            if(!tutoManager.saveTuto)
            {
                StartCoroutine(tutoManager.HandleToggleTipsUI("La méditation est votre alliée : elle vous permet de progresser en toute sécurité et de retrouver des forces"));
                tutoManager.saveTuto = true;
            }            
        }  
    }
}