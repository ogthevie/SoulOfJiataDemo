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
    float lifeDuration = 60f;


    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStats = playerManager.GetComponent<PlayerStats>();
        notificationManager = FindObjectOfType<NotificationManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }


    private void FixedUpdate()
    {
        float delta = Time.deltaTime;
        HandleLifeDuration(delta);
    }
    
    private void HandleLifeDuration(float delta)
    {
        if(lifeDuration <= 0f)
        {
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 1.5f);
        }


        if(lifeDuration > 0f && gameObject != null)
        {
            lifeDuration -= delta;
        }
    }

    private void HandleItemsByplayer()
    {
        //Debug.DrawRay(transform.position, Vector3.up * 3f, Color.red, 1f);
        if(Physics.SphereCast(transform.position, 1f, Vector3.up, out hit, 8f))
        {
            if(hit.collider.gameObject.layer == 3 )
            {
                if(consumableData.consumableName != "Ikok" && consumableData.consumableName != "Matango")
                {
                    if(consumableData.consumableName == "Mangue") k = 3;
                    else if(consumableData.consumableName == "Prune") k = 4;
                    else if(consumableData.consumableName == "Nkomo") k = 5;
                    else if(consumableData.consumableName == "Kola du lion") k = 6;
                    else if(consumableData.consumableName == "Kola du singe") k = 7;
                    else if(consumableData.consumableName == "Gesier") k = 8;
                    else if(consumableData.consumableName == "Mintoumba") k = 9;
                    else if(consumableData.consumableName == "Kalaba") k = 10;
                    else if(consumableData.consumableName == "Katorro") k = 11;

                    playerStats.AddHealth(notificationManager.consumableDatas[k].HealthPoint);
                    playerStats.AddStamina(notificationManager.consumableDatas[k].StaminaPoint);
                }
                else
                {
                    if(consumableData.consumableName == "Ikok")
                    {
                        k = 1;
                        inventory.ikokQty += 1;
                        
                    }
                    else if(consumableData.consumableName == "Matango")
                    {
                        k = 2;
                        inventory.matangoQty += 1;
                    }
                    
                    inventoryManager.HandleItemsQty();
                    playerManager.onTutoScreen = true;
                    audioManager.PickConsomable();
                }

                Destroy(gameObject);               
            }
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        if(consumableData.consumableName == "Nkomo" || consumableData.consumableName == "Mangue" || consumableData.consumableName == "Prune")
        {
            if(other.gameObject.layer == 9)
            {
                audioManager.ImpactconsumableFx();    
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
        HandleItemsByplayer();
        notificationManager.StartTuto(k);    
    }

}

