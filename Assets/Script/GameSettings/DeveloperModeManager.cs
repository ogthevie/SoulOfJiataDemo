using System.Collections.Generic;
using UnityEngine;

namespace SJ
{
    public class DeveloperModeManager : MonoBehaviour
    {
        InputManager inputManager;
        PlayerStats playerStats;
        public StatesCharacterData jiataCharacterData;
        public InventoryData inventoryData;
        public List <GameObject> vases = new ();
        public List <GameObject> tolols = new ();
        public GameObject kossi, kossiKaze;


        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerStats = GetComponent<PlayerStats>();
        }

        void Start()
        {
            inventoryData.canBaemb = inventoryData.canPur = inventoryData.canSomm = inventoryData.canDest = true;
        }

        public void HandleInstantiateVases()
        {
            if(inputManager.one_input)
            {
                Debug.Log("un vase est apparu");
                int i =  CheckTypesOfVases();
                Vector3 vasePosition = transform.position + new Vector3 (0.5f, 5f, 10f);
                Quaternion vaseRotaion = Quaternion.Euler(-90f, 0f, 0f);
                Instantiate(vases[i], vasePosition, vaseRotaion);
            }
        }

        public void HandleInstantiateKossi()
        {
           if(Input.GetKeyDown(KeyCode.N))
           {
                Debug.Log("un kossi est apparu");
                Vector3 kossiKazePosition = transform.position + new Vector3 (10f, 0f, -20f);
                Instantiate(kossiKaze, kossiKazePosition, Quaternion.identity);
           } 
        }

        public void HandleInstantiateKossiKaze()
        {
           if(Input.GetKeyDown(KeyCode.K))
           {
                Debug.Log("un kossiKaze est apparu");
                Vector3 kossiKazePosition = transform.position + new Vector3 (-10f, 0f, 20f);
                Instantiate(kossiKaze, kossiKazePosition, Quaternion.identity);
           } 
        }

        public void HandleInstantiateTolols()
        {
            if(inputManager.two_input)
            {
                Debug.Log("un Tolol est apparu");
                int i = CheckTypesOfTolols();
                Vector3 tololPosition = transform.position + new Vector3 (10f, 0f, -10f);
                Instantiate(tolols[i], tololPosition, Quaternion.identity);
            }
        }

        public void HandleStats()
        {
            if(inputManager.three_input && playerStats.currentHealth < playerStats.maxHealth)
                playerStats.AddHealth(1000);

            else if(inputManager.four_input && playerStats.currentStamina < playerStats.maxStamina)
                playerStats.AddStamina(1000);
        }

        public void HandleSorcery()
        {
            if(inputManager.five_input)
                inventoryData.canBaemb = inventoryData.canPur = inventoryData.canSomm = inventoryData.canDest = true;

            else if (inputManager.six_input)
            inventoryData.canBaemb = inventoryData.canPur = inventoryData.canSomm = inventoryData.canDest = false;

        }

        public void ResetInventory()
        {
            if(inputManager.seven_input)
                inventoryData.nkomoQty = inventoryData.pruneQty = inventoryData.mangueQty = inventoryData.mintoumbaQty = inventoryData.matangoQty = 
                inventoryData.kalabaQty = inventoryData.gesierQty = inventoryData.colaLionQty = inventoryData.colaSingeQty = inventoryData.odontolQty = inventoryData.katorroQty = inventoryData.ikokQty = 0;
        }

        static int CheckTypesOfVases()
        {
            int vaseIndex = Random.Range(0,4);
            return (vaseIndex);
        }

        static int CheckTypesOfTolols()
        {
            int tololIndex = Random.Range(0,3);
            return (tololIndex);
        }
    }
}

