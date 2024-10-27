using UnityEngine;
using SJ;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public ConsumableData consumableData;
    public NotificationManager notificationManager;
    PlayerManager playerManager;
    PlayerStats playerStats;
    public InventoryData inventory;
    AudioManager audioManager;
    InventoryManager inventoryManager;
    RaycastHit hit;
    int k;


    void Awake()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        playerStats = playerManager.GetComponent<PlayerStats>();
        notificationManager = FindFirstObjectByType<NotificationManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        audioManager = playerStats.GetComponent<AudioManager>();
    }

    private void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.up * 15f, ForceMode.Impulse);    
    }

    private void HandleItemsByplayer()
    {
        if(consumableData.consumableName == "Ikok" && inventory.ikokQty < 5)
        {
            k = 1;
            inventory.ikokQty += 1;
            
        }
        else if(consumableData.consumableName == "Sel de lempoy" && inventory.selQty < 5)
        {
            k = 2;
            inventory.selQty += 1;
        }

        audioManager.PickConsomable();
        inventoryManager.HandleItemsQty();
        
        playerManager.onTutoScreen = true;
        Destroy(gameObject);               
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 3)
        {
            HandleItemsByplayer();
            notificationManager.StartTuto(k); 
        }
 
    }

}

