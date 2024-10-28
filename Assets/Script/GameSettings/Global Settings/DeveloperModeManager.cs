using System.Collections.Generic;
using UnityEngine;

namespace SJ
{
    public class DeveloperModeManager : MonoBehaviour
    {
        InputManager inputManager;
        PlayerStats playerStats;
        GameSaveManager gameSaveManager;
        PlayerUIManager playerUIManager;
        AnimatorManager animatorManager;

        public StatesCharacterData jiataCharacterData;
        public List <GameObject> vases = new ();
        public List <GameObject> tolols = new ();
        public GameObject kossi, kossiKaze, keliper, kossiNanga;


        void Awake()
        {
            gameSaveManager = FindFirstObjectByType<GameSaveManager>();
            inputManager = GetComponent<InputManager>();
            playerStats = GetComponent<PlayerStats>();
            playerUIManager = FindFirstObjectByType<PlayerUIManager>();
            animatorManager = GetComponent<AnimatorManager>();
        }

        public void HandleInstantiateMobs()
        {
            #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("un Tolol est apparu");
                int i = CheckTypesOfTolols();
                Vector3 tololPosition = transform.position;
                Instantiate(tolols[i], tololPosition, Quaternion.identity);
            }

           if(Input.GetKeyDown(KeyCode.K))
           {
                Debug.Log("un kossiKaze est apparu");
                Vector3 kossiKazePosition = transform.position + new Vector3 (-10f, 0f, 20f);
                Instantiate(kossiKaze, kossiKazePosition, Quaternion.identity);
           }

           if(Input.GetKeyDown(KeyCode.L))
           {
                Debug.Log("un keliper est apparu");
                Vector3 keliperPosition = transform.position + new Vector3 (10f, 0f, 20f);
                Instantiate(keliper, keliperPosition, Quaternion.identity);
           }
           if(Input.GetKeyDown(KeyCode.N))
           {
                Debug.Log("un KossiNanga est apparu");
                Vector3 KossiNangaPosition = transform.position + new Vector3 (-10f, 0f, 10f);
                Instantiate(kossiNanga, KossiNangaPosition, Quaternion.identity);
           }
            #endif
        }

        public void HandleStats()
        {
            #if UNITY_EDITOR
            if(inputManager.three_input && playerStats.currentHealth <= playerStats.maxHealth)
            {
                playerStats.AddHealth(1000);
                //StartCoroutine(playerUIManager.HandleAchievement("Sauvegarde effectuée"));
                gameSaveManager.SaveAllData();
            }
            else if(inputManager.four_input && playerStats.currentStamina <= playerStats.maxStamina)
            {
                playerStats.AddStamina(1000);
            }
             #endif   
        }

        public void LoadSave()
        {
            #if UNITY_EDITOR
            if(inputManager.five_input)
                gameSaveManager.LoadAllData();
            #endif

        }

        public void ResetSave()
        {
            #if UNITY_EDITOR
            if(inputManager.seven_input)
                gameSaveManager.ClearAllSaves();
            #endif
        }

        static int CheckTypesOfTolols()
        {
            int tololIndex = Random.Range(0,2);
            return (tololIndex);
        }

        public void DancePlayer()
        {
            if(Input.GetKeyDown(KeyCode.Keypad1)) animatorManager.PlayTargetAnimation("dance 1", true);
            else if(Input.GetKeyDown(KeyCode.Keypad2)) animatorManager.PlayTargetAnimation("dance 2", true);
            else if(Input.GetKeyDown(KeyCode.Keypad3)) animatorManager.PlayTargetAnimation("dance 3", true);
            else if(Input.GetKeyDown(KeyCode.Keypad4)) animatorManager.PlayTargetAnimation("dance 4", true);
        }
    }
}
