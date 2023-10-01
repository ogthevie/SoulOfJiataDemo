using System.Collections.Generic;
using UnityEngine;


namespace SJ
{
    public class AudioManager : MonoBehaviour
    {
        PlayerAttacker playerAttacker;
        PlayerManager playerManager;
        public AudioSource jiataAudioSource;
        public AudioSource cameraAudioSource;
        public AudioSource interactionAudioSource;
        PlayerLocomotion playerLocomotion;
        [HideInInspector] public AudioClip footstepSound;
        public List<AudioClip> footStepSfx = new ();
        public List<AudioClip> jumpSfx = new ();
        public List<AudioClip> fightSfx = new ();
        public List<AudioClip> pause = new ();
        readonly float attackVolume = 0.15f;
        readonly float footStepVolume = 0.05f;
        readonly float jumpVolume = 0.05f;
        readonly float ThunderVolume= 0.3f;
        
        void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            jiataAudioSource = GetComponent<AudioSource>();
            playerManager = GetComponent<PlayerManager>();
        }
        
        void Start()
        {
            footstepSound = footStepSfx[0];
        }

        public void ReadWalkFx()
        {
            if(playerManager.isInteracting)
                return;

            jiataAudioSource.volume = footStepVolume;
            if(playerLocomotion.speed < 6)
            {
                jiataAudioSource.PlayOneShot(footstepSound);
            }
        }
        public void ReadRunFx()
        {
            if(playerManager.isInteracting)
                return;

            jiataAudioSource.volume = footStepVolume;
            if(playerLocomotion.speed == playerLocomotion.movementSpeed && !playerManager.isSprinting)
            {
                jiataAudioSource.PlayOneShot(footstepSound);
            }
        }

        public void ReadSprintFx()
        {
            if(playerManager.isInteracting)
                return;

            jiataAudioSource.volume = footStepVolume;
            if(playerManager.isSprinting)
            {
                jiataAudioSource.PlayOneShot(footstepSound);
                //Debug.Log("Je sprinte");
            }
            
        }
        public void ReadStartJumpFx()
        {
            jiataAudioSource.volume = footStepVolume;
            jiataAudioSource.PlayOneShot(footstepSound);   
        }

        public void ReadFightAttackFx()
        {
            jiataAudioSource.volume = attackVolume;

            if(playerAttacker.lastAttack == "HighAttack1")
            {
                jiataAudioSource.PlayOneShot(fightSfx[4]);
            }
            else if(playerAttacker.lastAttack == "HighAttack2")
                jiataAudioSource.PlayOneShot(fightSfx[5]);

            else if(playerAttacker.lastAttack == "HighAttack3")
                jiataAudioSource.PlayOneShot(fightSfx[6]); //l'espace 6 est vide

        }

        public void ReadLowFightAttackFx()
        {
            jiataAudioSource.PlayOneShot(fightSfx[0]);
        }

        public void ReadMagnetiRayFx()
        {
            jiataAudioSource.volume = footStepVolume;
            jiataAudioSource.PlayOneShot(fightSfx[3]);
        }

        public void ReadAttractionFx()
        {
            jiataAudioSource.volume = footStepVolume;
            jiataAudioSource.PlayOneShot(fightSfx[2]);
        }

        public void ReadSurchargeFx()
        {
            jiataAudioSource.volume = footStepVolume;
            jiataAudioSource.PlayOneShot(fightSfx[7]);
        }

        public void ReadDismissFx()
        {
            jiataAudioSource.volume = attackVolume;
            jiataAudioSource.PlayOneShot(fightSfx[8]);
        }

        public void ReadArcLightningFx()
        {

            cameraAudioSource.volume = attackVolume;
            cameraAudioSource.PlayOneShot(fightSfx[9]);

        }

        public void ReadThunderFx()
        {
            cameraAudioSource.volume = ThunderVolume;
            cameraAudioSource.PlayOneShot(fightSfx[10]);
        }

        public void ReadHitFx()
        {
            interactionAudioSource.volume = attackVolume;
            interactionAudioSource.PlayOneShot(fightSfx[13]);
        }

        public void ReadNavigationInventoryFx()
        {
            jiataAudioSource.volume = attackVolume;
            jiataAudioSource.PlayOneShot(pause[0]);
        }

        public void HealthRecoverFx()
        {
            cameraAudioSource.volume = attackVolume;
            cameraAudioSource.PlayOneShot(pause[1]);
        }

        public void ImpossibleChoiceFx()
        {
            jiataAudioSource.volume = attackVolume;
            jiataAudioSource.PlayOneShot(pause[2]);
        }

        public void NavigateMenuFx()
        {
            jiataAudioSource.volume = attackVolume;
            jiataAudioSource.PlayOneShot(pause[5]);
        }

        public void ImpactconsumableFx()
        {
            cameraAudioSource.volume = jumpVolume;
            cameraAudioSource.PlayOneShot(pause[4]);
        }

        public void PickConsomable()
        {
            interactionAudioSource.volume = jumpVolume;
            interactionAudioSource.PlayOneShot(pause[3]);
        }

        public void ActivateRune()
        {
            cameraAudioSource.volume = jumpVolume;
            cameraAudioSource.PlayOneShot(fightSfx[11]);
        }

        public void BreakVase()
        {
            interactionAudioSource.volume = attackVolume;
            interactionAudioSource.PlayOneShot(fightSfx[12]);
        }


    }
}


