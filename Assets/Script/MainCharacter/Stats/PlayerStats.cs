using System;
using UnityEngine;

namespace SJ
{
        public class PlayerStats : CharacterStats
    {
        public StatesCharacterData stateJiataData;
        InputManager inputManager;
        PlayerManager playerManager;
        PlayerLocomotion playerLocomotion;
        public CameraManager cameraManager;
        public StaminaBar staminaBar;
        public EnduranceBar enduranceBar;
        AnimatorManager animatorManager;

        public float coefRegenStamina = 5f;

        void Awake() 
        {
            inputManager = GetComponent<InputManager>();
            playerManager = GetComponent<PlayerManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
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


        public void TakeDamage(int damage)
        {
            if(stateJiataData.isIndomitable)
                return;

            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);

            if(currentHealth > 0 && !playerManager.isInAir) 
            {
                if(damage < 20) animatorManager.PlayTargetAnimation("Low Damage", true);
                else animatorManager.PlayTargetAnimation("High Damage", true);
            }

            else if(currentHealth <= 0)
            {
                currentHealth = 0;
                //playerLocomotion.moveDirection = Vector3.zero;
                animatorManager.PlayTargetAnimation ("Dead", true);
                playerManager.isDead = true;
                stateJiataData.isHidden = true;
            }

        }
        public void TakeStaminaDamage(int staminaDamage)
        {
            if(stateJiataData.isIndomitable) return;

            currentStamina -= staminaDamage;
            staminaBar.SetCurrentStamina(currentStamina);

            if(currentStamina <= 0) currentStamina = 0;

        }
        public void AddStamina(int StaminaUnit)
        {
            currentStamina += StaminaUnit;
            staminaBar.SetCurrentStamina(currentStamina);

            if(currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        public void AddHealth(int HealthUnit)
        {
            currentHealth += HealthUnit;
            healthBar.SetCurrentHealth(currentHealth);

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
                enduranceBar.slider.value -= 1f * Time.deltaTime;
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

