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
        InventoryManager inventoryManager;
        
        public InventoryData inventoryData;
        public StatesCharacterData statesJiataData;
        public List <Shader> jiataShaders = new();
        public List <SkinnedMeshRenderer> jiatabodyRenderer = new();
        [SerializeField] GameObject lefButton, rightButton;
        public bool sLeft, sRight;
        readonly int coefBoostBaemb = 2;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
            inventoryManager = FindObjectOfType<InventoryManager>();
        }

        void Update()
        {
            HandleSorcery();
        }

        void LateUpdate()
        {
            HandleActivateSlot();
        }

        void HandleActivateSlot()
        {
            if(inventoryData.ikokQty > 6 && playerManager.canSomm) lefButton.SetActive(true); //Activer le marqueur
            else lefButton.SetActive(false);

            if(inventoryData.selQty > 2 & playerManager.canBaemb) rightButton.SetActive(true);//Activer le marqueur
            else rightButton.SetActive(false);//Desactiver le marqueur
            
        }

        public void HandleSorcery()
        {         
            if(playerManager.canSomm && inputManager.left_input && inventoryData.ikokQty >= 7)
            {
                if(jiatabodyRenderer[10].material.shader == jiataShaders[3]) return;
                sLeft = true;
                HandleSorceryWestEffect();
                inventoryData.ikokQty -= 7;
            }

            else if(playerManager.canBaemb && inputManager.right_input && inventoryData.selQty >= 3)
            {
                //Debug.Log("yo");
                if(jiatabodyRenderer[10].material.shader == jiataShaders[2]) return;
                sRight = true;
                HandleSorceryEastEffect();
                inventoryData.selQty -= 3;
            }

            inventoryManager.HandleItemsQty();
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

        void HandleSorceryEastEffect()
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

