using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace SJ
{
    public class PlayerAttacker : MonoBehaviour
    {
        public CameraManager cameraManager;
        InputManager inputManager;
        AnimatorManager animatorManager;
        PlayerLocomotion playerLocomotion;
        public  PlayerStats playerStats;
        PlayerManager playerManager;
        public SorceryPadManager sorceryPadManager;
        public AudioManager audioManager;

        public StatesCharacterData statesJiataData;
        public string lastAttack;
        public GameObject magicRayOrigin, kikohaOrigin;
        [SerializeField] GameObject magnetiOriginGrab;
        public GameObject interactOriginRay;

        public GameObject arcLightningFx;
        public GameObject thunderFx, kikohaPrefab;
        public GameObject targetarcLightning;
        public GameObject targetThunder;
        public GameObject preThunder;
        ParticleSystem soul_Lituba_Fx, /*soul_Isango_Fx ,soul_Sokoto_Fx,*/ soul_Pefussep_Fx;

        [SerializeField] GameObject leffectLitubaFx, reffectLitubaFx;
        ParticleSystem.MainModule lMain;
        ParticleSystem.MainModule rMain;
        ParticleSystem fxLA;
        public ParticleSystem paralyzeBrassardFX, paralyzeFx, surchargeFX, smokeRecul, electrocutionFx;
        ParticleSystem auraFx;
        Color lekbaRuben = new(0.87f, 0.25f, 0.87f);
        Color lekbaRubenLituba = new(0.85f, 0.55f, 0.1f);

        readonly int highAttackDrain = 5;
        public readonly int magnetiDrain = 8;
        public readonly int arcLightningDrain = 25;
        public readonly int thunderDrain = 70;
        public readonly int kikohaDrain = 3;
        public readonly float magnetiMaxDistance = 30f;
        readonly float arcLightMaxDistance = 30f;
        readonly float thunderMaxDistance = 20; //20f
        readonly float magnetiRadius = 2f;
        readonly int magnetiDamage = 5;
        public Vector3 arclightbox = new (1f, 1f, 1f);
        readonly float thunderRadius = 15; //40f

        public bool isHit;
        public bool isTargetHit;

        public RaycastHit targetHit;
        public RaycastHit [] targetsThunderHits;
        public Material [] lightingMaterials;
        Transform fxHAOne, fxHATwo, fxHAThree;
        [SerializeField] GameObject haOne, haTwo, haThree;

        string[] lowAttackArray = new string[] { "LowAttack1", "LowAttack2"};

        private void Awake()
        {
            sorceryPadManager = GetComponent<SorceryPadManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerStats = GetComponent<PlayerStats>();
            //impactDetect = GetComponentInChildren<ImpactDetect>();
            animatorManager = GetComponent<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            playerManager = GetComponent<PlayerManager>();
            audioManager = GetComponent<AudioManager>();

            cameraManager = FindObjectOfType<CameraManager>();

            magicRayOrigin = transform.GetChild(20).gameObject;
            magnetiOriginGrab = transform.GetChild(24).gameObject;
            interactOriginRay = transform.GetChild(25).gameObject;
            //lowAttackOrigin = GameObject.Find("lowAttackOrigin");

            soul_Lituba_Fx = GameObject.Find("SoulLitubaFx").GetComponent<ParticleSystem>();
            //soul_Isango_Fx = GameObject.Find("SoulIsangoFx").GetComponent<ParticleSystem>();
            //soul_Sokoto_Fx = GameObject.Find("SoulSokotoFx").GetComponent<ParticleSystem>();
            soul_Pefussep_Fx = GameObject.Find("SoulPefussepFx").GetComponent<ParticleSystem>();

            leffectLitubaFx = GameObject.Find("LEffectSoulLitubaFx");
            reffectLitubaFx = GameObject.Find("REffectSoulLitubaFx");

            lMain = leffectLitubaFx.GetComponent<ParticleSystem>().main;
            rMain = reffectLitubaFx.GetComponent<ParticleSystem>().main;

            Transform brassard = transform.GetChild(19);

            fxHAThree = brassard.GetChild(0);
            fxHAOne = brassard.GetChild(3);
            fxHATwo = brassard.GetChild(1);
            fxLA = brassard.GetChild(2).GetComponent<ParticleSystem>();           

            //magnetiFX = magicRayOrigin.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            //surchargeFX = magicRayOrigin.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();

            auraFx = transform.GetChild(21).GetComponent<ParticleSystem>();


        }
        private void Start()
        {
            if(!arcLightningFx.activeSelf)
                arcLightningFx.SetActive(false);
            if(!thunderFx.activeSelf)
                thunderFx.SetActive(false);

            statesJiataData.d_LowAttack = 5;
            statesJiataData.d_HighAttack = 10;
            statesJiataData.d_ArcLight = 45;
            statesJiataData.d_Thunder = 160;
            statesJiataData.isIndomitable = false;
            statesJiataData.isHidden = false;
            
        }

        public void HandleLightAttack()
        {
            if (playerStats.currentStamina <= kikohaDrain)
                return;
            animatorManager.PlayTargetAnimation("LowAttack1", true);
            lastAttack = "LowAttack1";
        }

        public void HandleHighAttack()
        {
            if (playerStats.currentStamina <= highAttackDrain)
                return;
            animatorManager.PlayTargetAnimation("HighAttack1", true);
            lastAttack = "HighAttack1";
        }


        public void HandleCombo()
        {
            if (inputManager.comboFlag)
            {
                animatorManager.anim.SetBool("canDoCombo", false);

                if (lastAttack == "LowAttack1")
                {
                    if (inputManager.circle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("LowAttack2", true);
                        lastAttack = "LowAttack2";
                    }
                    else if (inputManager.triangle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("HighAttack1", true);
                        lastAttack = "HighAttack1"; // A revoir
                    }
                }
                else if (lastAttack == "LowAttack2")
                {
                    if (inputManager.circle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("LowAttack1", true);
                        lastAttack = "LowAttack1";
                    }
                    else if (inputManager.triangle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("HighAttack3", true);
                        lastAttack = "HighAttack3";
                    }
                }
                else if (lastAttack == "LowAttack2" && inputManager.circle && playerStats.currentStamina > 2)
                {
                        animatorManager.PlayTargetAnimation("LowAttack1", true);
                        lastAttack = "LowAttack1";
                }
                else if(lastAttack == "LowAttack2" && inputManager.triangle && playerStats.currentStamina > 2)
                {
                    animatorManager.PlayTargetAnimation("HighAttack3", true);
                    lastAttack = "HighAttack3";
                }


                if (lastAttack == "HighAttack1")
                {
                    if (inputManager.triangle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("HighAttack2", true);
                        lastAttack = "HighAttack2";
                    }
                    else if (inputManager.circle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("LowAttack2", true);
                        lastAttack = "LowAttack2";
                    }
                }
                else if (lastAttack == "HighAttack2")
                {
                    if (inputManager.triangle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("HighAttack3", true);
                        lastAttack = "HighAttack3";
                    }
                    else if (inputManager.circle && playerStats.currentStamina > 2)
                    {
                        animatorManager.PlayTargetAnimation("LowAttack2", true);
                        lastAttack = "LowAttack2";
                    }

                }
            }

        }

        public void FxHighAttack()
        {
            if(lastAttack == "HighAttack1") Instantiate(haOne, fxHAOne.transform.position, fxHAOne.transform.rotation);
            else if (lastAttack == "HighAttack2") Instantiate(haTwo, fxHATwo.transform.position, fxHATwo.transform.rotation);
            else if (lastAttack == "HighAttack3") Instantiate(haThree, fxHAThree.transform.position, fxHAThree.transform.rotation);

            playerStats.TakeStaminaDamage(highAttackDrain);

        }

        public void FxLowAttack()
        {

            if (lowAttackArray.Contains(lastAttack))
            {
                fxLA.Play();
                audioManager.ReadLowFightAttackFx();
            }
        }

        public void HandleKikohaAttack()
        {
            GameObject projectile = Instantiate(kikohaPrefab, kikohaOrigin.transform.position, kikohaPrefab.transform.rotation);
            Rigidbody pRigibody = projectile.GetComponent<Rigidbody>();
            Vector3 direction = kikohaOrigin.transform.forward;
            
            FxLowAttack();
            playerStats.TakeStaminaDamage(kikohaDrain);
            pRigibody.AddForce(direction * 70f, ForceMode.Impulse);
        }



        public void HandleMagicSkill()
        {
            if (animatorManager.anim.GetBool("isInteracting"))
                return;

            //LE MAGIC FLAG FLOTTE, XA PEUT AIDER!!!!!


            if (inputManager.surchargeFlag)
            {
                if (playerStats.currentStamina < magnetiDrain)
                    return;

                playerLocomotion.moveDirection = Vector3.zero;
                animatorManager.PlayTargetAnimation("Surcharge", true);
                
            }
            
            else if(inputManager.paralyzeFlag)
            {
                if (playerStats.currentStamina < magnetiDrain * 2)
                    return;
                playerLocomotion.moveDirection = Vector3.zero;
                animatorManager.PlayTargetAnimation("Paralyze", true);
            }

            else if(inputManager.arcLightFlag)
            {
                if (playerStats.currentStamina < arcLightningDrain)
                    return;

                playerLocomotion.moveDirection = Vector3.zero;
                animatorManager.PlayTargetAnimation("ArcLightning", true);
                
            }

            else if(inputManager.thunderFlag)
            {
                if (playerStats.currentStamina < thunderDrain)
                    return;
                playerLocomotion.moveDirection = Vector3.zero;
                animatorManager.PlayTargetAnimation("Thunder", true);
            }
        }
        
        public void ParalyzeRay()
        {
            Instantiate(paralyzeBrassardFX, magicRayOrigin.transform.position, Quaternion.identity);

            //Debug.DrawRay(magnetiOriginGrab.transform.position, magnetiOriginGrab.transform.forward * magnetiMaxDistance, Color.red, duration);
            if(Physics.SphereCast(magnetiOriginGrab.transform.position, magnetiRadius, magnetiOriginGrab.transform.forward, out RaycastHit hit, magnetiMaxDistance))
            {
                //Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject.layer ==  8)
                {
                    Instantiate(paralyzeFx, hit.point, Quaternion.identity);
                    StartCoroutine(StopMagnetSphere());

                    IEnumerator StopMagnetSphere()
                    {
                        hit.rigidbody.isKinematic = true;
                        yield return new WaitForSeconds (5f);
                        if(hit.collider.gameObject != null) hit.rigidbody.isKinematic = false;
                    }
                }

                else if(hit.collider.gameObject.layer == 10)
                {
                    if(hit.collider.TryGetComponent<VaseContainerManager>(out VaseContainerManager component)) StartCoroutine (StopVase());
                    
                    IEnumerator StopVase()
                    {
                        Vector3 spawnPosition = hit.collider.gameObject.transform.position + new Vector3 (0f, 0.4f, 0f);
                        Instantiate(paralyzeFx, spawnPosition, Quaternion.identity);
                        yield return new WaitForSeconds(4f);
                        component.HandleVaseConatinerProcess();
                    } 
                }

                else if(hit.collider.gameObject.layer == 12)
                {
                    if(hit.collider.TryGetComponent<EnemyManager>(out EnemyManager component))
                    {
                        if(component.isbreak || component is BuffaloManager buffaloManager) return;

                        Instantiate(paralyzeFx, component.lockOnTransform.position, Quaternion.identity);
                    
                        StartCoroutine (StopTime());
                        IEnumerator StopTime()
                        {
                            Animator animator = component.GetComponent<Animator>();
                            string animClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                            component.isbreak = true;
                            animator.speed = 0f;
                            yield return new WaitForSeconds (5.1f);
                            if(animator != null) animator.speed = 1f;
                            component.isbreak = false;
                            if(!playerManager.tutoManager.paralyzeTuto)
                            {
                                StartCoroutine(playerManager.tutoManager.HandleToggleTipsUI("Pertubez l'harmonie vibratoire de vos adversaires, vous les immobiliserez"));
                                playerManager.tutoManager.paralyzeTuto = true;
                            }
                        }
                    }
                }
                else if(hit.collider.gameObject.tag == "Stele")
                {
                    hit.collider.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = lightingMaterials[0];
                    hit.collider.gameObject.GetComponent<AudioSource>().PlayOneShot(audioManager.fightSfx[14]);
                    if(!playerManager.tutoManager.steleTuto)
                    {
                        StartCoroutine(playerManager.tutoManager.HandleToggleTipsUI("La lueur verte, illumine le chemin du héros. La jaune, trahit la férocité de la bête"));
                        playerManager.tutoManager.steleTuto = true;
                    }
                }   

            }
        }

        public void SurchargeRay()
        {
            float a = 1.5f;
            float b = 48f;
            Instantiate(surchargeFX, magicRayOrigin.transform.position, Quaternion.identity);
            //Debug.DrawRay(magnetiOriginGrab.transform.position, magnetiOriginGrab.transform.forward * magnetiMaxDistance, Color.red, duration);
            if(Physics.SphereCast(magnetiOriginGrab.transform.position, magnetiRadius, magnetiOriginGrab.transform.forward, out RaycastHit hit, magnetiMaxDistance))
            {
                float force = (a * hit.distance) + b; //y = ax+b
                if(hit.collider.gameObject.layer ==  8)
                {
                    hit.rigidbody.isKinematic = false;
                    hit.rigidbody.AddForce(this.transform.forward * force, ForceMode.Impulse);
                    Instantiate(surchargeFX, hit.point, Quaternion.identity);
                }

                else if(hit.collider.gameObject.layer == 10)
                {
                    Vector3 spawnPosition = hit.collider.gameObject.transform.position + new Vector3 (0f, 0.4f, 0f);
                    Instantiate(surchargeFX, spawnPosition, Quaternion.identity);
                    if(hit.collider.TryGetComponent<VaseContainerManager>(out VaseContainerManager component)) component.HandleVaseConatinerProcess();
                }

                else if(hit.collider.gameObject.layer == 12)
                {
                    if(hit.collider.TryGetComponent<EnemyManager>(out EnemyManager component))
                    {
                        Instantiate(surchargeFX, component.lockOnTransform.position, Quaternion.identity);

                        if(component is kossiKazeManager kossiKazeManager) kossiKazeManager.kossiKazePattern.HandleExplosion();
                        else if(component is KossiManager kossiManager)
                        {
                            kossiManager.TakeDamage(magnetiDamage);
                        }
                        else if(component is TololManager tololManager)
                        {
                            if(component.isbreak) return;
                            component.gameObject.GetComponent<TololAnimatorManager>().anim.SetBool("isACHit", true);  
                        }
                        else if(component is KeliperManager keliperManager) keliperManager.TakeDamage(2);
                    }
                }
                else if(hit.collider.gameObject.tag == "Stele")
                {
                    hit.collider.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = lightingMaterials[1];
                    hit.collider.gameObject.GetComponent<AudioSource>().PlayOneShot(audioManager.fightSfx[14]);
                }                

            }


        }

        public void ArcLightningRay()
        {
            //Debug.DrawRay(magicRayOrigin.transform.position, magicRayOrigin.transform.forward * arcLightMaxDistance, Color.yellow, duration);

            if(cameraManager.currentLockOnTarget != null)
            {
                if(cameraManager.currentLockOnTarget.TryGetComponent<EnemyManager>(out EnemyManager component))
                {
                    
                    if(component is TololManager tololManager)
                    {
                        ParticleSystem fx = Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                        fx.transform.SetParent(component.transform);
                        float distance = tololManager.tololPattern.distanceFromTarget;
                        arcLightningFx.SetActive(true);
                        smokeRecul.Play();
                        targetarcLightning.transform.position = tololManager.lockOnTransform.position;
     
                        if(!tololManager.isbreak) component.gameObject.GetComponent<TololAnimatorManager>().anim.SetBool("isACHit", true);

                        if(distance <= arcLightMaxDistance) tololManager.TakeDamage(statesJiataData.d_ArcLight); 
                        else tololManager.TakeDamage(statesJiataData.d_ArcLight/2);

                    }

                    else if(component is KossiManager kossiManager)
                    {
                        float distance = kossiManager.kossiPattern.distanceFromTarget;
                        arcLightningFx.SetActive(true);
                        smokeRecul.Play();
                        targetarcLightning.transform.position = kossiManager.lockOnTransform.position;

                        if(distance <= arcLightMaxDistance)kossiManager.TakeDamage(statesJiataData.d_ArcLight); 
                        else kossiManager.TakeDamage(statesJiataData.d_ArcLight/2); 

                    }

                    else if(component is KeliperManager keliperManager)
                    {
                        arcLightningFx.SetActive(true);
                        smokeRecul.Play();
                        targetarcLightning.transform.position = keliperManager.lockOnTransform.position;
                        float distance = keliperManager.keliperPattern.distanceFromTarget;

                        if(keliperManager.keliperPattern.currentTarget == null) 
                        {
                            keliperManager.keliperPattern.currentTarget = FindObjectOfType<PlayerManager>();
                            keliperManager.isPreformingAction = false;
                        }
                        if(distance <= arcLightMaxDistance)
                        {
                            ParticleSystem fx = Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                            fx.transform.SetParent(component.transform);
                            keliperManager.TakeDamage(statesJiataData.d_ArcLight);
                            keliperManager.keliperPattern.stunt = true;
                        }
                        else keliperManager.TakeDamage(statesJiataData.d_ArcLight/2);
                    }

                    else if(component is kossiKazeManager kossiKazeManager)
                    {
                            arcLightningFx.SetActive(true);
                            smokeRecul.Play();
                            targetarcLightning.transform.position = kossiKazeManager.lockOnTransform.position;
                            kossiKazeManager.kossiKazePattern.HandleExplosion();;
                    }
                    
                    else if(component is BuffaloManager buffaloManager)
                    {
                        float distance = buffaloManager.buffaloPattern.distanceFromTarget;
                        arcLightningFx.SetActive(true);
                        smokeRecul.Play();
                        targetarcLightning.transform.position = buffaloManager.lockOnTransform.position;

                        if(buffaloManager.isArmor) return;
                        
                        if(distance <= arcLightMaxDistance)
                        {
                            Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                            buffaloManager.iStun = true;
                            buffaloManager.TakeDamage(statesJiataData.d_ArcLight); 
                        }                              
                        else  buffaloManager.TakeDamage(statesJiataData.d_ArcLight/2);
                    }
                    if(!playerManager.tutoManager.arcLightTuto)
                    {
                        StartCoroutine(playerManager.tutoManager.HandleToggleTipsUI("L'onde de choc provoquée par le souffle de Shango assomme les ennemis et les laisse vulnérables"));
                        playerManager.tutoManager.arcLightTuto = true;
                    }
                }
            }

            else if(Physics.BoxCast(magicRayOrigin.transform.position, arclightbox * 5f, magicRayOrigin.transform.forward, out targetHit, Quaternion.identity, arcLightMaxDistance))//(Physics.SphereCast(magicRayOrigin.transform.position, arcLightRadius, magicRayOrigin.transform.forward, out hit, arcLightMaxDistance))
            {
                /*if(targetHit.collider.gameObject.layer == 8)
                {
                    StartCoroutine(HandleArcLightningEffect());
                }*/
                if(targetHit.collider.gameObject.layer == 10)
                {
                    arcLightningFx.SetActive(true);
                    smokeRecul.Play();
                    targetarcLightning.transform.position = targetHit.transform.position;

                    if(targetHit.collider.TryGetComponent<VaseContainerManager>(out VaseContainerManager component))
                    {
                        component.HandleVaseConatinerProcess();
                    }
                }
                else if(targetHit.collider.gameObject.layer == 12)
                {
                    if(targetHit.collider.gameObject.TryGetComponent<EnemyManager>(out EnemyManager component))
                    {
                        if(component is TololManager tololManager)
                        {
                            isTargetHit = true;
                            float distance = tololManager.tololPattern.distanceFromTarget;
                            targetarcLightning.transform.position = tololManager.lockOnTransform.position;
                            arcLightningFx.SetActive(true);
                            smokeRecul.Play();
                            

                            if(tololManager.tololPattern.currentTarget == null) 
                            {
                                tololManager.tololPattern.currentTarget = playerManager;
                                tololManager.isPreformingAction = false;
                            } 

                            if(distance <= arcLightMaxDistance)
                            {
                                ParticleSystem fx = Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                                fx.transform.SetParent(component.transform);
                                tololManager.TakeDamage(statesJiataData.d_ArcLight);
                                if(!tololManager.isbreak) component.gameObject.GetComponent<TololAnimatorManager>().anim.SetBool("isACHit", true);
                            }
                            else
                            {
                                ParticleSystem fx = Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                                fx.transform.SetParent(component.transform);
                                tololManager.TakeDamage(statesJiataData.d_ArcLight/2);
                                if(!tololManager.isbreak) component.gameObject.GetComponent<TololAnimatorManager>().anim.SetBool("isACHit", true);                                
                            }
                        }
                        else if(component is KossiManager kossiManager)
                        {
                            float distance = kossiManager.kossiPattern.distanceFromTarget;
                            arcLightningFx.SetActive(true);
                            smokeRecul.Play();
                            targetarcLightning.transform.position = kossiManager.lockOnTransform.position;

                            if(kossiManager.kossiPattern.currentTarget == null) 
                            {
                                kossiManager.kossiPattern.currentTarget = playerManager;
                                kossiManager.isPreformingAction = false;
                            }
                            if(distance <= arcLightMaxDistance) kossiManager.TakeDamage(statesJiataData.d_ArcLight);
                            else kossiManager.TakeDamage(statesJiataData.d_ArcLight/2);
                          
                        }

                        else if(component is KeliperManager keliperManager)
                        {
                            float distance = keliperManager.keliperPattern.distanceFromTarget;

                            arcLightningFx.SetActive(true);
                            smokeRecul.Play();
                            targetarcLightning.transform.position = keliperManager.lockOnTransform.position;

                            if(keliperManager.keliperPattern.currentTarget == null) 
                            {
                                keliperManager.keliperPattern.currentTarget = FindObjectOfType<PlayerManager>();
                                keliperManager.isPreformingAction = false;
                            }
                            if(distance <= arcLightMaxDistance)
                            {
                                keliperManager.TakeDamage(statesJiataData.d_ArcLight);
                                keliperManager.keliperPattern.stunt = true;
                                ParticleSystem fx = Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                                fx.transform.SetParent(component.transform);
                            }
                            else keliperManager.TakeDamage(statesJiataData.d_ArcLight/2);
                        }

                        else if(component is kossiKazeManager kossiKazeManager)
                        {
                            float distance = kossiKazeManager.kossiKazePattern.distanceFromTarget;

                            arcLightningFx.SetActive(true);
                            smokeRecul.Play();
                            targetarcLightning.transform.position = kossiKazeManager.lockOnTransform.position;
                            kossiKazeManager.kossiKazePattern.HandleExplosion();                       
                        }

                        else if(component is BuffaloManager buffaloManager)
                        {
                            if(buffaloManager.isArmor) return;

                            float distance = buffaloManager.buffaloPattern.distanceFromTarget;
                            arcLightningFx.SetActive(true);
                            smokeRecul.Play();
                            targetarcLightning.transform.position = buffaloManager.lockOnTransform.position;


                            if(distance <= arcLightMaxDistance)
                            {
                                buffaloManager.iStun = true;
                                ParticleSystem fx = Instantiate(electrocutionFx, component.lockOnTransform.position, Quaternion.identity);
                                fx.transform.SetParent(component.transform);
                                buffaloManager.TakeDamage(statesJiataData.d_ArcLight); 
                            }
                            else buffaloManager.TakeDamage(statesJiataData.d_ArcLight/2); 
                        }
                    }
                }
                else
                {
                    arcLightningFx.SetActive(true);
                    smokeRecul.Play();
                }
            }

            else
            {
                arcLightningFx.SetActive(true);
                smokeRecul.Play();
            }          
        }

        public void ThunderRay()
        {

            RaycastHit [] thunderHits = Physics.SphereCastAll(magicRayOrigin.transform.position, thunderRadius, magicRayOrigin.transform.forward, thunderMaxDistance);

            targetsThunderHits = thunderHits;

            foreach (RaycastHit hit in targetsThunderHits)
            {

                if(hit.collider.gameObject.layer == 12)
                {
                    if(hit.collider.gameObject.TryGetComponent<EnemyManager>(out EnemyManager component))
                    {
                        if(component is TololManager tololManager)
                        {
                            StartCoroutine(HandleThunderEffect(hit.transform.position));
                            tololManager.TakeDamage(statesJiataData.d_Thunder);
                            if(!tololManager.isbreak) tololManager.tololAnimatorManager.anim.SetBool("isACHit", true);    
                            //Debug.Log(thunderHits.Length);            A REVOIR
                            tololManager.tololPattern.currentTarget = playerManager;
                            tololManager.isPreformingAction = false;                        
                        }
                        if(component is KossiManager kossiManager)
                        {
                            StartCoroutine(HandleThunderEffect(hit.transform.position));
                            kossiManager.TakeDamage(statesJiataData.d_Thunder); 
                            //Debug.Log(thunderHits.Length);            A REVOIR                             
                        }
                        if(component is kossiKazeManager kossiKazeManager)
                        {
                            StartCoroutine(HandleThunderEffect(hit.transform.position));
                            kossiKazeManager.isDead = true;
                        }
                        if(component is KeliperManager keliperManager)
                        {
                            StartCoroutine(HandleThunderEffect(hit.transform.position));
                            keliperManager.TakeDamage(statesJiataData.d_Thunder);
                            keliperManager.keliperPattern.stunt = true;
                            if(keliperManager.keliperPattern.currentTarget == null) 
                            {
                                keliperManager.keliperPattern.currentTarget = FindObjectOfType<PlayerManager>();
                                keliperManager.isPreformingAction = false;
                            }
                        }
                        if(component is BuffaloManager buffaloManager)
                        {
                            StartCoroutine(HandleThunderEffect(buffaloManager.lockOnTransform.position));
                            if(buffaloManager.isArmor) return;
                            buffaloManager.TakeDamage(statesJiataData.d_Thunder);
                            buffaloManager.iStun = true;
                        }
                        if(!playerManager.tutoManager.thunderTuto)
                        {
                            StartCoroutine(playerManager.tutoManager.HandleToggleTipsUI("Le cri du ciel est une punition divine qui frappe et disperse les esprits maléfiques"));
                            playerManager.tutoManager.thunderTuto = true;
                        }
                    }
                }
            }

            //cameraManager.ClearLockOnTargets();

            //}

            
            IEnumerator HandleThunderEffect(Vector3 tempsPosition)
            {
                ThunderDrainStamina();
                thunderFx.SetActive(true);
                smokeRecul.Play();
                audioManager.ReadThunderFx();
                yield return new WaitForSeconds(0.05f);
                targetThunder.transform.position = tempsPosition;
                yield return new WaitForSeconds(1f);
                thunderFx.SetActive(false);
            }
        }

        public void HandlePreThunder()
        {
            StartCoroutine(HandlePreThunderEffect());

            IEnumerator HandlePreThunderEffect()
            {
                preThunder.SetActive(true);
                yield return new WaitForSeconds(2.2f);
                preThunder.SetActive(false);
            }
        }

        public void DisableArcLightningFx()
        {
            if(arcLightningFx != null)
            {
                arcLightningFx.SetActive(false);
            }
                
        }


        public void PlayAuraFx()
        {
            auraFx.Play();
        }

        public void StopAuraFx()
        {
            auraFx.Stop();
        }

        public void PlayEffectLitubaFx()
        {
            lMain.startColor = lekbaRubenLituba;
            rMain.startColor = lekbaRubenLituba;
            
            if(playerManager.haveGauntlet) leffectLitubaFx.GetComponent<ParticleSystem>().Play();
            reffectLitubaFx.GetComponent<ParticleSystem>().Play();
        }

        public void StopEffectLitubaxFx()
        {
            lMain.startColor = lekbaRuben;
            rMain.startColor = lekbaRuben;

            leffectLitubaFx.GetComponent<ParticleSystem>().Stop();
            reffectLitubaFx.GetComponent<ParticleSystem>().Stop();            
        }

        public void MagnetiDrainStamina()
        {
            playerStats.TakeStaminaDamage(magnetiDrain);
        }

        public void LightningDrainStamina()
        {
            playerStats.TakeStaminaDamage(arcLightningDrain);
        }
        public void ThunderDrainStamina()
        {
            playerStats.TakeStaminaDamage(thunderDrain);
        }

        public void HandleSorceryPad()
        {
            if(sorceryPadManager.sLeft)
            {
                soul_Pefussep_Fx.Play();
                sorceryPadManager.sLeft = false;
            }
       
            else if(sorceryPadManager.sRight)
            {
                soul_Lituba_Fx.Play();
                sorceryPadManager.sRight = false;
            }
        }

        public void HandlePowerUpFx()
        {
            Instantiate(paralyzeBrassardFX, magicRayOrigin.transform.position, Quaternion.identity);
        }

        public void HandlePowerUpBaembFx()
        {
            Instantiate(surchargeFX, magicRayOrigin.transform.position, Quaternion.identity);
        }

    }
}

