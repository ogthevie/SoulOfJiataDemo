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
        
        public InventoryData inventoryData;
        public StatesCharacterData statesJiataData;
        public GameObject sorceryUp, sorceryDown, sorceryLeft, sorceryRight;

        public List <Shader> jiataShaders = new();
        public List <SkinnedMeshRenderer> jiatabodyRenderer = new();

        public bool sUp, sDown, sLeft, sRight;
        readonly int coefBoostBaemb = 2;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();

            sorceryUp = GameObject.Find("SoulSokoto");
            sorceryDown = GameObject.Find("SoulIsango");
            sorceryLeft = GameObject.Find("SoulPefussep");
            sorceryRight = GameObject.Find("SoulLituba");

        }

        void Start()
        {

        }

        void LateUpdate()
        {
            float delta = Time.deltaTime;
            HandlePlayVFXSorcery();
            HandleSorcery();
        }

        void HandlePlayVFXSorcery()
        {
                /*if(inventoryData.pruneQty > 4 && inventoryData.kalabaQty > 1) sorceryUp.SetActive(true);
                else sorceryUp.SetActive(false);*/
            
                /*if(inventoryData.ikokQty > 0) sorceryDown.SetActive(true);
                else sorceryDown.SetActive(false);*/

                if(inventoryData.mangueQty > 4 && inventoryData.colaSingeQty > 2) sorceryLeft.SetActive(true);
                else sorceryLeft.SetActive(false);

                if(inventoryData.nkomoQty > 6 && inventoryData.mintoumbaQty > 4) sorceryRight.SetActive(true);
                else sorceryRight.SetActive(false);
            
        }

        public void HandleSorcery()
        {

            /*if(sorceryUp.activeSelf && inputManager.up_input)
            {
                sorceryUp.GetComponent<ParticleSystem>().Stop();
                sUp = true;
                sorceryUp.SetActive(false);
                HandleSorceryUpEffect();
                inventoryData.pruneQty -= 4;
                inventoryData.kalabaQty -=1; 
            }*/


            /*else if(sorceryDown.activeSelf && inputManager.down_input)
            {
                sorceryDown.GetComponent<ParticleSystem>().Stop();
                sorceryDown.SetActive(false);
            }*/
            
            if(playerManager.canSomm)
            {
                if(sorceryLeft.activeSelf && inputManager.left_input)
                {
                    if(jiatabodyRenderer[12].material.shader == jiataShaders[3]) return;
                    sorceryLeft.GetComponent<ParticleSystem>().Stop();
                    sLeft = true;
                    sorceryLeft.SetActive(false);
                    HandleSorceryLeftEffect();
                    inventoryData.mangueQty -= 4;
                    inventoryData.colaSingeQty -= 2;
                    
                }
            }

            if(playerManager.canBaemb)
            {
                if(sorceryRight.activeSelf && inputManager.right_input)
                {
                    if(jiatabodyRenderer[12].material.shader == jiataShaders[2]) return;
                    sorceryRight.GetComponent<ParticleSystem>().Stop();
                    sRight = true;
                    sorceryRight.SetActive(false);
                    HandleSorceryRightEffect();
                    inventoryData.nkomoQty -= 6;
                    inventoryData.mintoumbaQty -= 4;
                }
            }

        }

        /*void HandleSorceryUpEffect()
        {
            StartCoroutine(effect());
            
            IEnumerator effect()
            {
                int i;

                for (i = 0; i < jiatabodyRenderer.Count; i ++)
                {
                    jiatabodyRenderer[i].material.shader = jiataShaders[2];
                 
                }
                statesJiataData.isIndomitable = true;
                playerAttacker.playerStats.AddHealth(1000);
                playerAttacker.playerStats.AddStamina(1000);
                
                //ajouter un son ici
                yield return new WaitForSeconds(25f);

                for (i = 0; i < (jiatabodyRenderer.Count - 1); i++)
                {
                    jiatabodyRenderer[i].material.shader = jiataShaders[0];
                    jiatabodyRenderer[12].material.shader = jiataShaders[1];
                }

                statesJiataData.isIndomitable = false;
                
            }
        }*/

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
                    jiatabodyRenderer[12].material.shader = jiataShaders[1];
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

        //destins croisés

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

