using UnityEngine;
using TMPro;

namespace SJ
{
    public class InventoryManager : MonoBehaviour
    {
        InputManager inputManager;
        AudioManager audioManager;
        PlayerStats playerStats;
        PlayerManager playerManager;
        public ConsumableData ikokData, selData;
        public InventoryData inventory;
        public TextMeshProUGUI ikokQty, selQty;

        void Start()
        {
            inputManager = FindFirstObjectByType<InputManager>();
            playerStats = inputManager.GetComponent<PlayerStats>();
            playerManager = inputManager.GetComponent<PlayerManager>();
            audioManager = inputManager.GetComponent<AudioManager>();
            HandleItemsQty();
        }
       
        void LateUpdate()
        {
            HandleImproveQty();
        }
        
        public void HandleItemsQty()
        {
            ikokQty.text = inventory.ikokQty.ToString();
            selQty.text = inventory.selQty.ToString();            
        }

        public void HandleImproveQty()
        {
            if(playerManager.onOption) return;

            if(inputManager.left_input) 
            {
                if(playerStats.currentStamina < playerStats.maxStamina && inventory.selQty > 0)
                {
                    playerStats.AddStamina(selData.StaminaPoint);
                    audioManager.StatRecoverFx();
                    inventory.selQty -= 1;
                    HandleItemsQty();
                }

            }
            
            if(inputManager.right_input)
            {
                if(playerStats.currentHealth < playerStats.maxHealth && inventory.ikokQty > 0)
                {
                    playerStats.AddHealth(ikokData.HealthPoint);
                    audioManager.StatRecoverFx();
                    inventory.ikokQty -= 1;
                    HandleItemsQty(); 
                }
               
            }
        }

    }
}

