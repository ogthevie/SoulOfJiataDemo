using UnityEngine;
/* ------------------------      BUTTON MAPPING          ----------------------------------
    L1 -> Attraction +square -> Magneti ||  +triangle -> ThunderDrop ||  +cross -> arcLightning || +circle -> surcharge
    L2 -> Cancel
    R1 -> Recharge(à retirer)
    R2 -> Course, rouler, stepback
    L3 -> Verouillé cible
    R3 -> ...
    westButton -> Interagir
    EastButton -> lowAttack
    NorthButton -> highAttack
    southButton -> Sauter 
    up -> grisgrisOne
    down -> grisgrisThree
    left -> grisgrisFour
    right -> grisgrisTwo
*/
namespace SJ
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls playerControls;
        PlayerAttacker playerAttacker;
        PlayerManager playerManager;
        CameraManager cameraManager;
        PlayerLocomotion playerLocomotion;
        public StatesCharacterData jiatastats;
        PlayerStats playerStats;

        #region variables
        public Vector2 movementInput;
        public Vector2 cameraInput;
        public float mouseX;
        public float mouseY;
        public float moveAmount;
        public float vertical;
        public float horizontal;
        
        public bool right_Stick_Right_input;
        public bool right_Stick_Left_input;
        public bool lockOn_input;
        public bool south_input;
        public bool rt_input;
        public bool lowAttack_input;
        public bool highAttack_input;
        //public bool rb_input;
        public bool lb_input;
        public bool lt_input;
        public bool north_input;
        public bool west_input;
        public bool right_input;
        public bool left_input;
        public bool up_input;
        public bool down_input;
        public bool left_menu_input;
        public bool right_menu_input;
        public bool left_Stick_input;
        public bool start_input;

        float magicInputTimer;

        public bool upFlag, downFlag, leftFlag, rightFlag;
        public bool flipFlag;
        public bool sprintFlag;

        public bool circle;
        public bool triangle;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool specialAttackFlag;
        public bool magicFlag;
        public bool magicAttackFlag;
        public bool magnetiFlag;
        public bool surchargeFlag;
        public bool arcLightFlag;
        public bool thunderFlag;
        public bool InteractFlag;
        
        #endregion

        #region  DevMode

        public bool one_input, two_input, three_input, four_input, five_input, six_input, seven_input;

        #endregion

        private void Awake() 
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerAttacker = GetComponent<PlayerAttacker>();  
            playerManager = GetComponent<PlayerManager>();
            cameraManager = FindObjectOfType<CameraManager>();
            playerStats = GetComponent<PlayerStats>();
            playerManager.haveGauntlet = false;
        }
        

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.LockOn.performed += i => lockOn_input = true;
                playerControls.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_input = true;
                playerControls.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_input = true;
                playerControls.PlayerActions.LowAttack.performed += i => lowAttack_input = true;
                playerControls.PlayerActions.HighAttack.performed += i => highAttack_input = true;
                playerControls.PlayerActions.Interact.performed += i => west_input = true;
                playerControls.PlayerActions.Jump.performed += i => south_input = true;
                playerControls.PlayerActions.Slide.performed += i => rt_input = true;
                playerControls.PlayerMovement.InventoryMovementRight.performed += i => right_input =  true;
                playerControls.PlayerMovement.InventoryMovementLeft.performed += i => left_input =  true;
                playerControls.PlayerMovement.InventoryMovementUp.performed += i => up_input =  true;
                playerControls.PlayerMovement.InventoryMovementDown.performed += i => down_input =  true;
                playerControls.PlayerActions.OnPause.performed += i => start_input = true;
                playerControls.PlayerMovement.NavigateLeftMenu.performed += i => left_menu_input = true;
                playerControls.PlayerMovement.NavigateRightMenu.performed += i => right_menu_input = true;

                /*playerControls.PlayerMovement.InventoryMovementUp.performed += i => up_input = true;
                playerControls.PlayerMovement.InventoryMovementDown.performed += i => down_input = true;
                playerControls.PlayerMovement.InventoryMovementLeft.performed += i => left_input = true;
                playerControls.PlayerMovement.InventoryMovementRight.performed += i => right_input = true;*/


                playerControls.DevMode.AddVases.performed += i => one_input = true;
                playerControls.DevMode.AddTolol.performed += i => two_input = true;
                playerControls.DevMode.MaxHealth.performed += i => three_input = true;
                playerControls.DevMode.MaxStamina.performed += i => four_input = true;
                playerControls.DevMode.ActivateSorcery.performed += i => five_input = true;
                playerControls.DevMode.ResetSorcery.performed += i => six_input = true;
                playerControls.DevMode.ResetInventory.performed += i => seven_input = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public void TickInput(float delta)
        {
            HandleDisablePauseMenu();
            HandleEnablePauseMenu();

            if(Time.timeScale == 0f)
                return;

            HandleMoveInput(delta);
            HandleSprintInput();
            HandleAttackInput(delta);
            HandleLockOnInput();
            HandleFlipInput(delta);
            HandleMagicInput(delta);
            HandleMagnetiInput();
            HandleSurchargeInput();
            HandleArcLighInput();
            HandleThunderInput();
        }

        private void HandleMoveInput (float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleFlipInput(float delta)
        {
            
            if(rt_input && !sprintFlag) flipFlag = true;
            else flipFlag = false;

        }

        private void HandleSprintInput()
        {
            lt_input = playerControls.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            if(lt_input) sprintFlag = true;
            else sprintFlag = false;
        }
        private void HandleAttackInput(float delta)
        {
            if(playerManager.onPause) return;
            
            //playerControls.PlayerActions.LowAttack.performed += i => lowAttack_input = true;
            //playerControls.PlayerActions.HighAttack.performed += i => highAttack_input = true;

            if(lowAttack_input)
            {
                circle = true;
                if(magicAttackFlag)
                    return;
                    
                if(playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleCombo();
                    
                    comboFlag = false;
                }
                else
                {
                    if(playerManager.isInteracting)
                        return;
                    if(playerManager.canDoCombo)
                        return;
                    if(!playerManager.canAttack)
                        return;

                    playerAttacker.HandleLightAttack();
                }     
            }
            
            if(highAttack_input)
            {
                triangle = true;
                if(magicAttackFlag)
                    return;
                    
                if(playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleCombo();
                    
                    comboFlag = false;
                }
                else
                {
                    if(playerManager.isInteracting)
                        return;
                    if(playerManager.canDoCombo)
                        return;
                    if(!playerManager.canAttack)
                        return;

                    playerAttacker.HandleHighAttack();
                }
                

            }
            if(!lowAttack_input) circle = false;
            if(!highAttack_input) triangle = false;
        }

        private void HandleLockOnInput()
        {
            if(lockOn_input && lockOnFlag == false)
            {
                lockOn_input = false;
                cameraManager.HandleLockOn();
                if(cameraManager.nearestLockOnTarget != null)
                {
                    cameraManager.currentLockOnTarget = cameraManager.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if(lockOn_input && lockOnFlag)
            {
                lockOn_input = false;
                lockOnFlag = false;
                cameraManager.ClearLockOnTargets();
            }

            if (lockOnFlag && right_Stick_Left_input)
            {
                right_Stick_Left_input = false;
                cameraManager.HandleLockOn();
                if(cameraManager.leftLockTarget != null)
                {
                    cameraManager.currentLockOnTarget = cameraManager.leftLockTarget;
                }
            }

            if(lockOnFlag && right_Stick_Right_input)
            {
                right_Stick_Right_input = false;
                cameraManager.HandleLockOn();
                if(cameraManager.rightLockTarget != null)
                {
                    cameraManager.currentLockOnTarget = cameraManager.rightLockTarget;
                }
            }

            cameraManager.SetCameraHeight();
        }
        private void HandleMagicInput(float delta)
        {
            
            if(playerLocomotion.inAirTimer > 0)
                return;
            
            if (playerStats.currentStamina < 1)
                return;
            
            lb_input = playerControls.PlayerActions.UseMagic.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            
            if(lb_input)
            {
                magicInputTimer += delta; //Debug.Log(magicInputTimer);
                if(magicInputTimer > 0.4f)
                {
                    magicFlag = true;
                    magicAttackFlag = false;
                }
                else
                {
                    magicFlag = false;
                    magicAttackFlag = true; //Debug.Log("magicAttackFlag " +magicAttackFlag);
                }
            }
            else
                magicInputTimer = 0;

            if(!lb_input && magicInputTimer == 0)
                magicAttackFlag = false;

            playerAttacker.HandleMagicSkill();
        }

        private void HandleMagnetiInput()
        {
            if(!playerManager.haveGauntlet)
                return;
            if(magicAttackFlag && west_input) magnetiFlag = true;    
            else magnetiFlag = false;
        }

        private void HandleSurchargeInput()
        {
            if(!playerManager.haveGauntlet)
                return;

            if(magicAttackFlag && circle) surchargeFlag = true;
            else surchargeFlag = false;
        }

        private void HandleArcLighInput()
        { 
            if(!playerManager.canArcLight)
                return;

            if(magicAttackFlag && south_input) arcLightFlag = true;     
            else arcLightFlag = false;   
        }

        private void HandleThunderInput()
        {
            if(!playerManager.canThunder)
                return;
            if(magicAttackFlag && triangle) thunderFlag = true;
            else thunderFlag = false;
        }

        public void HandleInteractInput()
        {
            if(west_input)  InteractFlag = true;
            else InteractFlag = false;
        }

        private void HandleEnablePauseMenu()
        {
            if(start_input && !playerManager.onPause)
                playerManager.onPause = true;
        }
        private void HandleDisablePauseMenu()
        {
            if(playerManager.onPause)
            {
                if(south_input) playerManager.onPause = false;
            }
        }
    }
}
