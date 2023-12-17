using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
        SkillTreeManager skillTreeManager;
        
        public InventoryData inventoryData;
        public StatesCharacterData statesJiataData;
        public List <Shader> jiataShaders = new();
        public List <SkinnedMeshRenderer> jiatabodyRenderer = new();
         public bool preSomm, preBaemb;
        public bool sLeft, sRight;
        readonly int coefBoostBaemb = 2;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
            skillTreeManager = FindObjectOfType<SkillTreeManager>();
        }

        void LateUpdate()
        {
            HandleActivateSlot();
            HandleSorcery();
        }

        void HandleActivateSlot()
        {
            if(inventoryData.mangueQty > 4 && inventoryData.colaSingeQty > 2 && playerManager.canSomm) skillTreeManager.westSlot.enabled = true; //Activer le marqueur
            else skillTreeManager.westSlot.enabled = false;

            if(inventoryData.nkomoQty > 6 && inventoryData.mintoumbaQty > 4 && playerManager.canBaemb) skillTreeManager.eastSlot.enabled = true;//Activer le marqueur
            else skillTreeManager.eastSlot.enabled = false;//Desactiver le marqueur
            
        }

        public void HandleSorcery()
        {         
            if(skillTreeManager.westSlot.enabled && inputManager.left_input)
            {
                if(jiatabodyRenderer[10].material.shader == jiataShaders[3]) return;
                sLeft = true;
                HandleSorceryLeftEffect();
                inventoryData.mangueQty -= 4;
                inventoryData.colaSingeQty -= 2;
                
            }

            else if(skillTreeManager.eastSlot.enabled && inputManager.right_input)
            {
                if(jiatabodyRenderer[10].material.shader == jiataShaders[2]) return;
                sRight = true;
                HandleSorceryRightEffect();
                inventoryData.nkomoQty -= 6;
                inventoryData.mintoumbaQty -= 4;
            }
        }

        void HandleSorceryLeftEffect()
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
                    jiatabodyRenderer[12].material.shader = jiataShaders[1];
                }

                statesJiataData.isHidden = false;
                
            }

        }

        void HandleSorceryRightEffect()
        {
            StartCoroutine(effect());
            
            IEnumerator effect()
            {
                int i;

                for (i = 0; i < jiatabodyRenderer.Count; i ++) jiatabodyRenderer[i].material.shader = jiataShaders[2];

                statesJiataData.isIndomitable = true;
                playerLocomotion.movementSpeed *= 1.7f;
                playerLocomotion.sprintSpeed *= 1.7f;
                HandleUpStatsAttack(coefBoostBaemb);
                playerAttacker.PlayAuraFx();
                animatorManager.anim.SetFloat("BoostBaemb", 1.15f);
                playerAttacker.PlayEffectLitubaFx();
                //ajouter un son ici
                yield return new WaitForSeconds(30f);

                for (i = 0; i < (jiatabodyRenderer.Count - 1); i++)
                {
                    jiatabodyRenderer[i].material.shader = jiataShaders[0];
                    jiatabodyRenderer[10].material.shader = jiataShaders[1];
                }
                playerLocomotion.movementSpeed = 6f;
                playerLocomotion.sprintSpeed = 11f;
                HandleDownStatsAttack(coefBoostBaemb);
                playerAttacker.StopAuraFx();
                playerAttacker.StopEffectLitubaxFx();
                animatorManager.anim.SetFloat("BoostBaemb", 1f);
                statesJiataData.isIndomitable = false;
                
            }

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

