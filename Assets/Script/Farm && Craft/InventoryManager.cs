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
            inputManager = FindObjectOfType<InputManager>();
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

            if(inputManager.up_input) 
            {
                if(playerStats.currentHealth == playerStats.maxHealth || inventory.ikokQty < 1) audioManager.ImpossibleChoiceFx();
                else
                {
                    playerStats.AddHealth(ikokData.HealthPoint);
                    inventory.ikokQty -= 1;
                    HandleItemsQty();
                }

            }
            else if(inputManager.down_input)
            {
                if(playerStats.currentStamina == playerStats.maxStamina || inventory.selQty < 1) audioManager.ImpossibleChoiceFx();
                else
                {
                    playerStats.AddStamina(selData.StaminaPoint);
                    inventory.selQty -= 1;
                    HandleItemsQty(); 
                }
               
            }
        }

    }
}

