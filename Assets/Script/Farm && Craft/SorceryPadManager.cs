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

            sorceryUp = GameObject.Find("SoulSokoto");
            sorceryDown = GameObject.Find("SoulIsango");
            sorceryLeft = GameObject.Find("SoulPefussep");
            sorceryRight = GameObject.Find("SoulLituba");

            sorceryUp.SetActive(false);
            sorceryDown.SetActive(false);
            sorceryRight.SetActive(false);
            sorceryLeft.SetActive(false);

        }

        void Start()
        {

        }

        void Update()
        {
            float delta = Time.deltaTime;
            HandlePlayVFXSorcery();
            HandleSorcery();
        }

        void HandlePlayVFXSorcery()
        {
            if(!sorceryUp.activeSelf)
                if(inventoryData.pruneQty > 4 && inventoryData.kalabaQty > 1)
                {
                    sorceryUp.SetActive(true);
                    sorceryUp.GetComponent<ParticleSystem>().Play();
                }
            
            if(!sorceryDown.activeSelf)
                if(inventoryData.ikokQty > 0)
                {
                    sorceryDown.SetActive(true);
                    sorceryDown.GetComponent<ParticleSystem>().Play();
                }

            if(!sorceryLeft.activeSelf)
                if(inventoryData.mangueQty > 4 && inventoryData.colaSingeQty > 2)
                {
                    sorceryLeft.SetActive(true);
                    sorceryLeft.GetComponent<ParticleSystem>().Play();
                }

            if(!sorceryRight.activeSelf)
                if(inventoryData.nkomoQty > 6 && inventoryData.mintoumbaQty > 4)
                {
                    sorceryRight.SetActive(true);
                    sorceryRight.GetComponent<ParticleSystem>().Play();
                }
            
        }

        public void HandleSorcery()
        {
            if(sorceryUp.activeSelf && inputManager.up_input)
            {
                sorceryUp.GetComponent<ParticleSystem>().Stop();
                sorceryUp.SetActive(false);
                HandleSorceryUpEffect();     
            }

            else if(sorceryDown.activeSelf && inputManager.down_input)
            {
                sorceryDown.GetComponent<ParticleSystem>().Stop();
                sorceryDown.SetActive(false);
            }
            
            else if(sorceryLeft.activeSelf && inputManager.left_input)
            {
                sorceryLeft.GetComponent<ParticleSystem>().Stop();
                sorceryLeft.SetActive(false);
                HandleSorceryLeftEffect();
                
            }

            else if(sorceryRight.activeSelf && inputManager.right_input)
            {
                sorceryRight.GetComponent<ParticleSystem>().Stop();
                sorceryRight.SetActive(false);
                HandleSorceryRightEffect();
            }
        }

        void HandleSorceryUpEffect()
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

                playerLocomotion.movementSpeed *= 1.7f;
                playerLocomotion.sprintSpeed *= 1.7f;
                HandleUpStatsAttack(coefBoostBaemb);
                playerAttacker.PlayAuraFx();
                animatorManager.anim.SetFloat("BoostBaemb", 1.15f);
                playerAttacker.PlayEffectLitubaFx();
                //ajouter un son ici
                yield return new WaitForSeconds(30f);

                playerLocomotion.movementSpeed = 6f;
                playerLocomotion.sprintSpeed = 11f;
                HandleDownStatsAttack(coefBoostBaemb);
                playerAttacker.StopAuraFx();
                playerAttacker.StopEffectLitubaxFx();
                animatorManager.anim.SetFloat("BoostBaemb", 1f);
                
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

