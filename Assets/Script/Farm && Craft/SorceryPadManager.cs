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


        public List <Image> sorcerPalettes = new();
        public List <Shader> jiataShaders = new();
        public List <SkinnedMeshRenderer> jiatabodyRenderer = new();

        public bool sUp, sDown, sLeft, sRight;
        public float timeS_Up = 45f, timeS_Down = 45f, timeS_Left = 45f, timeS_Right = 45f;
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

            sorcerPalettes[0] = GameObject.Find("SNorth").GetComponent<Image>();
            sorcerPalettes[1] = GameObject.Find("SEast").GetComponent<Image>();
            sorcerPalettes[2] = GameObject.Find("SSouth").GetComponent<Image>();
            sorcerPalettes[3] = GameObject.Find("SWest").GetComponent<Image>();
        }

        void Start()
        {
            sorceryUp.SetActive(false);
            sorceryDown.SetActive(false);
            sorceryRight.SetActive(false);
            sorceryLeft.SetActive(false);
            sorcerPalettes[0].sprite = statesJiataData.sNorth;
            sorcerPalettes[1].sprite = statesJiataData.sEast;
            sorcerPalettes[2].sprite = statesJiataData.sSouth;
            sorcerPalettes[3].sprite = statesJiataData.sWest;
        }

        void Update()
        {
            float delta = Time.deltaTime;
            HandlePlayVFXSorcery();
            HandleSorcery();
            HandleTimer(delta);
        }

        void HandlePlayVFXSorcery()
        {
            if(!sorceryUp.activeSelf)
                if(inventoryData.canPur && timeS_Up == 45f)
                {
                    sorceryUp.SetActive(true);
                    sorceryUp.GetComponent<ParticleSystem>().Play();
                }
            
            if(!sorceryDown.activeSelf)
                if(inventoryData.canDest && timeS_Down == 45f)
                {
                    sorceryDown.SetActive(true);
                    sorceryDown.GetComponent<ParticleSystem>().Play();
                }

            if(!sorceryLeft.activeSelf)
                if(inventoryData.canSomm && timeS_Left == 45f)
                {
                    sorceryLeft.SetActive(true);
                    sorceryLeft.GetComponent<ParticleSystem>().Play();
                }

            if(!sorceryRight.activeSelf)
                if(inventoryData.canBaemb && timeS_Right == 45f)
                {
                    sorceryRight.SetActive(true);
                    sorceryRight.GetComponent<ParticleSystem>().Play();
                }
            
        }

        public void HandleSorcery()
        {
            if(sorceryUp.activeSelf && inputManager.upFlag)
            {
                sorceryUp.GetComponent<ParticleSystem>().Stop();
                sorceryUp.SetActive(false);

                if(timeS_Up > 0 )
                {
                    sUp = true;
                    timeS_Up -= 1;
                    inputManager.upFlag = false;
                    HandleSorceryUpEffect(); 
                }        
            }

            else if(sorceryDown.activeSelf && inputManager.downFlag)
            {
                sorceryDown.GetComponent<ParticleSystem>().Stop();
                sorceryDown.SetActive(false);

                if(timeS_Down > 0)
                {
                    sDown = true;
                    timeS_Down -= 1;
                    inputManager.downFlag = false;
                }
            }
            
            else if(sorceryLeft.activeSelf && inputManager.leftFlag)
            {
                sorceryLeft.GetComponent<ParticleSystem>().Stop();
                sorceryLeft.SetActive(false);

                if(timeS_Left > 0)
                {
                    sLeft = true;
                    timeS_Left -= 1;
                    inputManager.leftFlag = false;
                    HandleSorceryLeftEffect();
                }
            }

            else if(sorceryRight.activeSelf && inputManager.rightFlag)
            {
                sorceryRight.GetComponent<ParticleSystem>().Stop();
                sorceryRight.SetActive(false);

                if(timeS_Right > 0)
                {
                    sRight = true;
                    timeS_Right -= 1;
                    inputManager.rightFlag = false;
                    HandleSorceryRightEffect();
                }
            }
        }

        void HandleTimer(float delta)
        {
            if(timeS_Up > 0f && timeS_Up < 45f) timeS_Up -= delta;
            else timeS_Up = 45f;

            if(timeS_Down > 0 && timeS_Down < 45f) timeS_Down -= delta;
            else timeS_Down = 45f;

            if(timeS_Left > 0 && timeS_Left < 45f) timeS_Left -= delta;
            else timeS_Left = 45f;

            if(timeS_Right > 0 && timeS_Right < 45f) timeS_Right -= delta;
            else timeS_Right = 45f;

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

