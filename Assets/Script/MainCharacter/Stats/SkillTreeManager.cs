using UnityEngine;
using UnityEngine.UI;

namespace SJ
{
    public class SkillTreeManager : MonoBehaviour
    {
        StaminaBar staminaBar;
        PlayerAttacker playerAttacker;
        PlayerManager playerManager;
        public Image northSlot;
        public Image southSlot;
        public Image westSlot;
        public Image eastSlot;

        void Awake()
        {
            staminaBar = FindObjectOfType<StaminaBar>();
            playerAttacker = FindObjectOfType<PlayerAttacker>();
            playerManager = playerAttacker.GetComponent<PlayerManager>();
        }

        public void HandleActivateSlot()
        {
            northSlot.enabled = playerManager.canThunder && staminaBar.slider.value >= playerAttacker.thunderDrain;
            southSlot.enabled = playerManager.canArcLight && staminaBar.slider.value >= playerAttacker.arcLightningDrain;
        }
    }
}

