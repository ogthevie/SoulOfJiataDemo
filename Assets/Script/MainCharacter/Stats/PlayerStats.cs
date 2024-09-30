using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SJ
{
        public class PlayerStats : CharacterStats
    {
        public StatesCharacterData stateJiataData;
        InputManager inputManager;
        PlayerManager playerManager;
        PlayerLocomotion playerLocomotion;
        AudioManager audioManager;
        public CameraManager cameraManager;
        public StaminaBar staminaBar;
        public EnduranceBar enduranceBar;
        AnimatorManager animatorManager;
        [SerializeField] GameObject healthFx, staminaFx, impactSommFx;

        public float coefRegenStamina = 5f;

        void Awake() 
        {
            inputManager = GetComponent<InputManager>();
            playerManager = GetComponent<PlayerManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            audioManager = GetComponent<AudioManager>();
            staminaBar = FindObjectOfType<StaminaBar>();
            healthBar = FindObjectOfType<HealthBar>();
            enduranceBar = FindObjectOfType<EnduranceBar>();
            cameraManager = FindObjectOfType<CameraManager>();    
        }
        
        void Start()
        {
            maxHealth = SetMaxHealthFromhealthLevel();
            maxStamina = SetMaxStaminaFromStaminaLevel();
            maxEndurance = SetMaxEnduranceFromEnduranceLevel();

            currentHealth = maxHealth;
            currentStamina = maxStamina;
            currentEndurance = maxEndurance;

            enduranceBar.SetMaxEndurance(maxEndurance);
            healthBar.SetMaxHealth(maxHealth);
            staminaBar.SetMaxStamina(maxStamina);
        }

        private int SetMaxHealthFromhealthLevel()
        {
            maxHealth = healthLevel * 50;
            return maxHealth;
        }

        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        private float SetMaxEnduranceFromEnduranceLevel()
        {
            maxEndurance = enduranceLevel * 10;
            return maxEndurance;
        }


        public void TakeDamage(int damage, int levelDamage)
        {
            if(playerManager.isDead) return;

            if(stateJiataData.isHidden)
            {
                if(cameraManager.currentLockOnTarget != null)
                {
                    Instantiate(impactSommFx, cameraManager.currentLockOnTarget.lockOnTransform);
                    cameraManager.currentLockOnTarget.TakeDamage(damage*2);
                }
            }
            else
            {
                currentHealth -= damage;

                healthBar.SetCurrentHealth(currentHealth);

                if(currentHealth > 0 && !playerManager.isInAir) 
                {
                    if(levelDamage == 0) animatorManager.PlayTargetAnimation("Low Damage", true);
                    else animatorManager.PlayTargetAnimation("Middle Damage", true);
                }

                else if(currentHealth <= 0)
                {
                    currentHealth = 0;
                    //playerLocomotion.moveDirection = Vector3.zero;
                    animatorManager.PlayTargetAnimation ("Dead", true);
                    cameraManager.ClearLockOnTargets();
                    playerManager.isDead = true;
                    stateJiataData.isHidden = true;
                    if(SceneManager.GetActiveScene().buildIndex == 2) GameObject.Find("PortalKao").transform.GetChild(0).GetComponent<AudioSource>().Stop();
                    else if(SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        GameObject.Find("SibongoManager").GetComponent<AudioSource>().Stop();
                        GameObject.Find("StatutUmNyobe").GetComponent<AudioSource>().Stop();
                    } 
                }
            }


        }
        public void TakeStaminaDamage(int staminaDamage)
        {
            if(stateJiataData.isHidden) return;

            currentStamina -= staminaDamage;
            staminaBar.SetCurrentStamina(currentStamina);

            if(currentStamina <= 0) currentStamina = 0;
        }
        
        public void AddStamina(int StaminaUnit)
        {
            if(playerManager.isDead) return;

            currentStamina += StaminaUnit;
            staminaBar.SetCurrentStamina(currentStamina);
            audioManager.StatRecoverFx();
            Vector3 impactPosition = transform.position + new Vector3 (0f, 1f, 0f);
            Instantiate(staminaFx, impactPosition, Quaternion.identity);

            if(currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        public void AddHealth(int HealthUnit)
        {
            if(playerManager.isDead) return;
            
            currentHealth += HealthUnit;
            healthBar.SetCurrentHealth(currentHealth);
            audioManager.StatRecoverFx();
            Vector3 impactPosition = transform.position + new Vector3 (0f, 1.75f, 0f);
            Instantiate(healthFx, impactPosition, Quaternion.identity);
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public void HandleEndurance()
        {
            enduranceBar.SetCurrentEndurance(currentEndurance);

            if(playerLocomotion.speed == playerLocomotion.sprintSpeed && currentEndurance > 0 && !stateJiataData.isIndomitable)
            {
                enduranceBar.slider.value -= 0.5f * Time.deltaTime;
                currentEndurance = enduranceBar.slider.value;       
            }
            else if(!inputManager.sprintFlag && currentEndurance < maxEndurance)
            {
                enduranceBar.slider.value += 2f * Time.deltaTime;
                currentEndurance = enduranceBar.slider.value;  
            }      
        }


        public void HandleReloadStamina(float delta)
        {
            if(playerManager.isInteracting)
                return;

            if(staminaBar.slider.value < maxStamina)
            {
                staminaBar.slider.value += coefRegenStamina * delta;
                currentStamina = (int)Math.Truncate(staminaBar.slider.value);
            }

            if(currentStamina <= 0)
            {
                currentStamina = 0;
            } 
        }

    }
}

