using UnityEngine;
using UnityEngine.UI;

namespace SJ
{
    public class SkillTreeManager : MonoBehaviour
    {
        StaminaBar staminaBar;
        PlayerAttacker playerAttacker;
        public Image northSlot;
        public Image southSlot;
        public Image westSlot;
        public Image eastSlot;

        void Awake()
        {
            staminaBar = FindObjectOfType<StaminaBar>();
            playerAttacker = FindObjectOfType<PlayerAttacker>();
        }

        public void HandleActivateSlot()
        {
            if(staminaBar.slider.value < playerAttacker.thunderDrain)
                northSlot.enabled = false;
            else
                northSlot.enabled = true;
            

            if(staminaBar.slider.value < playerAttacker.extDomaineDrain)
                eastSlot.enabled = false;
            else
                eastSlot.enabled = true;

            if(staminaBar.slider.value < playerAttacker.magnetiDrain)
                westSlot.enabled = false;
            else
                westSlot.enabled = true;

            if(staminaBar.slider.value < playerAttacker.arcLightningDrain)
                southSlot.enabled = false;
            else
                southSlot.enabled = true;
        }
    }
}

