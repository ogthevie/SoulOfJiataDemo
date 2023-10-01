using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SJ
{
    public class InventoryManager : MonoBehaviour
    {
        InputManager inputManager;
        AudioManager audioManager;
        PlayerManager playerManager;
        PlayerStats playerStats;
        GridLayoutGroup gridLayoutGroup;
        Selectable currentSelectable;
        public Transform slotSelector;
        int i;
        [HideInInspector] public List<GameObject> slotConsumables = new ();
        [HideInInspector] public List<ConsumableData> consumableDatas = new ();
        public InventoryData inventoryData;
        public Image spriteDescription; 
        public TextMeshProUGUI nameDescription;
        public TextMeshProUGUI textDescription;
        public TextMeshProUGUI healthDescription;
        public TextMeshProUGUI staminaDeescription;
        

        void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            playerStats = FindObjectOfType<PlayerStats>();
            audioManager = FindObjectOfType<AudioManager>();
        }
        private void Start()
        {
            i = 0;
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
            currentSelectable = gridLayoutGroup.transform.GetChild(i).GetComponent<Selectable>();
            currentSelectable.Select();
            slotSelector.position = currentSelectable.transform.position;
        }

        private void Update()
        {
            if(!playerManager.onInventory)
                return;
            HandleMovementInventory();
            HandleQuantityInventory();
            HandleDescriptionInventory(i);
            AddStatsByConsumable(i);
            
        }

        public void HandleMovementInventory()
        {
            if (inputManager.up_input)
            {
                if( i < 4)
                    return;

                currentSelectable = gridLayoutGroup.transform.GetChild(i-4).GetComponent<Selectable>();
                i -= 4;
                HandleUpdateSelectorPosition();
            }
            else if (inputManager.down_input)
            {
                if(i > 7 && i < 12)
                    return;

                currentSelectable = gridLayoutGroup.transform.GetChild(i+4).GetComponent<Selectable>();
                i += 4;
                HandleUpdateSelectorPosition();
            }
            else if (inputManager.left_input)
            {
                if(i == 0)
                    return;

                currentSelectable = gridLayoutGroup.transform.GetChild(i-1).GetComponent<Selectable>();
                i -= 1;
                HandleUpdateSelectorPosition();
            }
            else if (inputManager.right_input)
            {
                //SelectNextElement(navigation.selectOnRight);
                if(i >= 11)
                {
                    i = 0;
                    currentSelectable = gridLayoutGroup.transform.GetChild(i).GetComponent<Selectable>(); 
                }
                else
                {
                    currentSelectable = gridLayoutGroup.transform.GetChild(i+1).GetComponent<Selectable>();
                    i += 1;          
                }
                HandleUpdateSelectorPosition();
            }
        }


        private void HandleQuantityInventory()
        {
            slotConsumables[0].GetComponentInChildren<TextMeshProUGUI>().text =  inventoryData.nkomoQty.ToString();
            slotConsumables[1].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.pruneQty.ToString();
            slotConsumables[2].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.mangueQty.ToString();
            slotConsumables[3].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.mintoumbaQty.ToString();
            slotConsumables[4].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.matangoQty.ToString();
            slotConsumables[5].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.kalabaQty.ToString();
            slotConsumables[6].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.gesierQty.ToString();
            slotConsumables[7].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.colaSingeQty.ToString();
            slotConsumables[8].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.colaLionQty.ToString();
            slotConsumables[9].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.odontolQty.ToString();
            slotConsumables[10].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.katorroQty.ToString();
            slotConsumables[11].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.ikokQty.ToString();


        }

        private void HandleDescriptionInventory(int k)
        {
            nameDescription.text = consumableDatas[k].consumableName.ToString();
            spriteDescription.sprite = consumableDatas[k].consumablesprite;
            textDescription.text = consumableDatas[k].ConsumableDescription;
            healthDescription.text = consumableDatas[k].HealthPoint.ToString();
            staminaDeescription.text = consumableDatas[k].StaminaPoint.ToString();
        }

        public void AddStatsByConsumable(int j)
        {
            if(inputManager.south_input)//// Parametrer pause
                {
                    if(playerStats.currentHealth >= playerStats.maxHealth && playerStats.currentStamina >= playerStats.maxStamina)
                    {
                        audioManager.ImpossibleChoiceFx();
                    }
                    else if(slotConsumables[j].GetComponentInChildren<TextMeshProUGUI>().text != "0")
                    {
                        int reductable = int.Parse(slotConsumables[j].GetComponentInChildren<TextMeshProUGUI>().text);
                        reductable -= 1;
                        if(j == 0)
                            inventoryData.nkomoQty = reductable;
                        else if(j == 1)
                            inventoryData.pruneQty = reductable;
                        else if(j == 2)
                            inventoryData.mangueQty = reductable;
                        else if(j == 3)
                            inventoryData.mintoumbaQty = reductable;
                        else if(j == 4)
                            inventoryData.matangoQty = reductable;
                        else if(j == 5)
                            inventoryData.kalabaQty = reductable;
                        else if(j == 6)
                            inventoryData.gesierQty = reductable;
                        else if(j == 7)
                            inventoryData.colaSingeQty = reductable;
                        else if(j == 8)
                            inventoryData.colaLionQty = reductable;
                        else if(j == 9)
                            inventoryData.odontolQty = reductable;
                        else if(j == 10)
                            inventoryData.katorroQty = reductable;
                        else if(j == 11)
                            inventoryData.ikokQty = reductable;

                        //EditorUtility.SetDirty(inventoryData);
                        
                        playerStats.AddHealth(consumableDatas[j].HealthPoint);
                        audioManager.HealthRecoverFx();
                        playerStats.AddStamina(consumableDatas[j].StaminaPoint);
                    }
                }
                
        }

        void HandleUpdateSelectorPosition()
        {
            currentSelectable.Select();
            slotSelector.position = currentSelectable.transform.position;
            audioManager.ReadNavigationInventoryFx();            
        }

    }
}

