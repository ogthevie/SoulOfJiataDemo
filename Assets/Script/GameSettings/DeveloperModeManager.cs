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

        public StatesCharacterData jiataCharacterData;
        public List <GameObject> vases = new ();
        public List <GameObject> tolols = new ();
        public GameObject kossi, kossiKaze;


        void Awake()
        {
            gameSaveManager = FindObjectOfType<GameSaveManager>();
            inputManager = GetComponent<InputManager>();
            playerStats = GetComponent<PlayerStats>();
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        public void HandleInstantiateVases()
        {
            #if UNITY_EDITOR
            if(inputManager.one_input)
            {
                Debug.Log("un vase est apparu");
                int i =  CheckTypesOfVases();
                Vector3 vasePosition = transform.position + new Vector3 (0.5f, 5f, 10f);
                Quaternion vaseRotaion = Quaternion.Euler(-90f, 0f, 0f);
                Instantiate(vases[i], vasePosition, vaseRotaion);
            }
            #endif
        }

        public void HandleInstantiateKossi()
        {
            #if UNITY_EDITOR
           if(Input.GetKeyDown(KeyCode.N))
           {
                Debug.Log("un kossi est apparu");
                Vector3 kossiKazePosition = transform.position + new Vector3 (10f, 0f, -20f);
                Instantiate(kossi, kossiKazePosition, Quaternion.identity);
           }
           #endif
        }

        public void HandleInstantiateKossiKaze()
        {
            #if UNITY_EDITOR
           if(Input.GetKeyDown(KeyCode.K))
           {
                Debug.Log("un kossiKaze est apparu");
                Vector3 kossiKazePosition = transform.position + new Vector3 (-10f, 0f, 20f);
                Instantiate(kossiKaze, kossiKazePosition, Quaternion.identity);
           }
           #endif
        }

        public void HandleInstantiateTolols()
        {
            #if UNITY_EDITOR
            if(inputManager.two_input)
            {
                Debug.Log("un Tolol est apparu");
                int i = CheckTypesOfTolols();
                Vector3 tololPosition = transform.position + new Vector3 (10f, 0f, -10f);
                Instantiate(tolols[i], tololPosition, Quaternion.identity);
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

        static int CheckTypesOfVases()
        {
            
            int vaseIndex = Random.Range(0,4);
            return (vaseIndex);
        }

        static int CheckTypesOfTolols()
        {
            int tololIndex = Random.Range(0,1);
            return (tololIndex);
        }
    }
}
