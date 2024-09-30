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

        [SerializeField] Image sorceryCanva;
        [SerializeField] GameObject controller, dialogUI;
        [SerializeField] Color showingColor, hidingColor;


        void Awake()
        {
            staminaBar = FindObjectOfType<StaminaBar>();
            sorceryCanva = GetComponent<Image>();
        }

        void Start()
        {
            dialogUI = GameObject.Find("PlayerUI").transform.GetChild(7).gameObject;
            playerAttacker = FindObjectOfType<PlayerAttacker>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void HandleActivateSlot()
        {
            northSlot.enabled = playerManager.canThunder && staminaBar.slider.value >= playerAttacker.thunderDrain;
            southSlot.enabled = playerManager.canArcLight && staminaBar.slider.value >= playerAttacker.arcLightningDrain;
            westSlot.enabled = playerManager.canSurcharge && staminaBar.slider.value >= playerAttacker.magnetiDrain;
            //eastSlot.enabled = playerManager.canBigFire && staminaBar.slider.value >= playerAttacker.bigFireDrain;
        }

        public void HandleSkillTreeUI(bool lockOnFlag)
        {   
            controller.SetActive(lockOnFlag);
            if(lockOnFlag)
            {
                RectTransform tempTp = this.GetComponent<RectTransform>();
                tempTp.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 0.5f);
                tempTp.DOAnchorPosX(320, 0.2f, false);
                tempTp.DOAnchorPosY(-320, 0.2f, false);

                sorceryCanva.color = northSlot.color = southSlot.color = westSlot.color = eastSlot.color = showingColor;

            }
            else
            {
                if(dialogUI.activeSelf) return;
                RectTransform tempTp = this.GetComponent<RectTransform>();
                tempTp.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
                tempTp.DOAnchorPosX(200, 0.2f, false);
                tempTp.DOAnchorPosY(-480, 0.2f, false);                
                controller.SetActive(false);

                sorceryCanva.color = northSlot.color = southSlot.color = westSlot.color = eastSlot.color = hidingColor;
            }
        }
    }
}

