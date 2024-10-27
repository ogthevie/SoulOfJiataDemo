using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ
{
    public class SorceryPadManager : MonoBehaviour
    {
        InputManager inputManager;
        PlayerAttacker playerAttacker;
        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;
        AnimatorManager animatorManager;
        
        public InventoryData inventoryData;
        public StatesCharacterData statesJiataData;
        public List <Shader> jiataShaders = new();
        public List <SkinnedMeshRenderer> jiatabodyRenderer = new();
        public bool sLeft, sRight;
        readonly int coefBoostBaemb = 2;
        [SerializeField] ParticleSystem IndomitableMode, normalMode;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
        }

        void HandleSorceryWestEffect()
        {
            StartCoroutine(effect());
            
            IEnumerator effect()
            {
                int i;

                for (i = 0; i < jiatabodyRenderer.Count; i ++)
                {
                    jiatabodyRenderer[i].material.shader = jiataShaders[3];
                 
                }
                statesJiataData.isHidden = true;
                
                //ajouter un son ici
                yield return new WaitForSeconds(30f);

                for (i = 0; i < (jiatabodyRenderer.Count - 1); i++)
                {
                    jiatabodyRenderer[i].material.shader = jiataShaders[0];
                    jiatabodyRenderer[10].material.shader = jiataShaders[1];
                }

                statesJiataData.isHidden = false;
                
            }

        }
        public void HandleSorceryEastEffect()
        {
            if(!playerManager.haveGauntlet) return;
            
            if(!statesJiataData.isIndomitable) EnableSorceryEastEffect();
            else DisableSorceryEastEffect();
        }

        void EnableSorceryEastEffect()
        {
                if(playerAttacker.playerStats.currentStamina > 5)
                {
                    IndomitableMode.Play();

                    int i;

                    for (i = 0; i < jiatabodyRenderer.Count; i ++) jiatabodyRenderer[i].material.shader = jiataShaders[2];

                    statesJiataData.isIndomitable = true;
                    playerLocomotion.movementSpeed *= 1.7f;
                    HandleUpStatsAttack(coefBoostBaemb);
                    playerAttacker.PlayAuraFx();
                    animatorManager.anim.SetFloat("BoostBaemb", 1.15f);
                    playerAttacker.PlayEffectLitubaFx();
                    playerManager.playerStats.AddStamina(5);
                    //ajouter un son ici
                }


        }

        public void DisableSorceryEastEffect()
        {
                normalMode.Play();
                playerAttacker.StopAuraFx();
                playerAttacker.StopEffectLitubaxFx();
                int i;

                for (i = 0; i < jiatabodyRenderer.Count; i++) jiatabodyRenderer[i].material.shader = jiataShaders[0];

                playerLocomotion.movementSpeed = 6f;
                HandleDownStatsAttack(coefBoostBaemb);
                animatorManager.anim.SetFloat("BoostBaemb", 1f);
                statesJiataData.isIndomitable = false;
        }

        void HandleUpStatsAttack(int coeff)
        {
            statesJiataData.d_LowAttack *= coeff;
            statesJiataData.d_HighAttack *= coeff;
            statesJiataData.d_ArcLight *= coeff;
            statesJiataData.d_Thunder *= coeff;
        }

        void HandleDownStatsAttack(int coeff)
        {
            statesJiataData.d_LowAttack /= coeff;
            statesJiataData.d_HighAttack /= coeff;
            statesJiataData.d_ArcLight /= coeff;
            statesJiataData.d_Thunder /= coeff;
        }
    }
}

