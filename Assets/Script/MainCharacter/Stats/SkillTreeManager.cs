using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        }

        void Start()
        {
            playerAttacker = FindObjectOfType<PlayerAttacker>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void HandleActivateSlot()
        {
            northSlot.enabled = playerManager.canThunder && staminaBar.slider.value >= playerAttacker.thunderDrain;
            southSlot.enabled = playerManager.canArcLight && staminaBar.slider.value >= playerAttacker.arcLightningDrain;
            westSlot.enabled = playerManager.haveGauntlet && staminaBar.slider.value >= playerAttacker.magnetiDrain;
            eastSlot.enabled = staminaBar.slider.value >= playerAttacker.magnetiDrain;
        }

        public void HandleSkillTreeUI(bool lockOnFlag)
        {
            if(lockOnFlag) this.GetComponent<RectTransform>().DOAnchorPosX(200, 0.4f, false);
            else this.GetComponent<RectTransform>().DOAnchorPosX(-250, 0.4f, false);
        }
    }
}

