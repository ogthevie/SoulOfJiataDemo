using UnityEngine;
using TMPro;

namespace SJ
{
    public class InventoryManager : MonoBehaviour
    {
        InputManager inputManager;
        AudioManager audioManager;
        PlayerManager playerManager;
        PlayerStats playerStats;
        public ConsumableData ikokData, matangoData;
        public InventoryData inventory;
        public TextMeshProUGUI ikokQty, matangoQty;

        void Start()
        {
            inputManager = FindObjectOfType<InputManager>();
            playerStats = inputManager.GetComponent<PlayerStats>();
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
            matangoQty.text = inventory.matangoQty.ToString();            
        }

        public void HandleImproveQty()
        {
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
                if(playerStats.currentStamina == playerStats.maxStamina || inventory.matangoQty < 1) audioManager.ImpossibleChoiceFx();
                else
                {
                    playerStats.AddStamina(matangoData.StaminaPoint);
                    inventory.matangoQty -= 1;
                    HandleItemsQty(); 
                }
               
            }
        }

    }
}

