using UnityEngine;
using SJ;

public class ItemManager : MonoBehaviour
{
    public ConsumableData consumableData;
    public NotificationManager notificationManager;
    PlayerManager playerManager;
    public InventoryData inventory;
    AudioManager audioManager;
    RaycastHit hit;
    int k;
    float lifeDuration = 60f;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        notificationManager = FindObjectOfType<NotificationManager>();
    }


    private void FixedUpdate()
    {
        float delta = Time.deltaTime;
        HandleLifeDuration(delta);
    }
    
    private void HandleLifeDuration(float delta)
    {
        if(lifeDuration <= 0f)
            Destroy(gameObject);

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
                if(consumableData.consumableName != "Ikok" && consumableData.consumableName != "Katorro")
                    audioManager.PickConsomable();
                    
                if(consumableData.consumableName == "Nkomo")
                {
                    k = 1;
                    playerManager.onTutoScreen = true;
                    inventory.nkomoQty += 1;
                }

                else if(consumableData.consumableName == "Prune")
                {
                    k = 2;
                    playerManager.onTutoScreen = true;
                    inventory.pruneQty += 1;
                }

                else if(consumableData.consumableName == "Mangue")
                {
                    k = 3;
                    playerManager.onTutoScreen = true;
                    inventory.mangueQty += 1;
                }

                else if(consumableData.consumableName == "Mintoumba")
                {
                    k = 4;
                    playerManager.onTutoScreen = true;
                    inventory.mintoumbaQty += 1;
                }

                else if(consumableData.consumableName == "Matango")
                {
                    k = 5;
                    playerManager.onTutoScreen = true;
                    inventory.matangoQty += 1;
                }

                else if(consumableData.consumableName == "Gesier")
                {
                    k = 6;
                    playerManager.onTutoScreen = true;
                    inventory.gesierQty += 1;
                }

                else if(consumableData.consumableName == "Kalaba")
                {
                    k = 7;
                    playerManager.onTutoScreen = true;
                    inventory.kalabaQty += 1;
                }

                else if(consumableData.consumableName == "Odontol")
                {
                    k = 8;
                    playerManager.onTutoScreen = true;
                    inventory.odontolQty += 1;
                }

                else if(consumableData.consumableName == "Kola du singe")
                {
                    k = 9;
                    playerManager.onTutoScreen = true;
                    inventory.colaSingeQty += 1;
                }

                else if(consumableData.consumableName == "Kola du lion")
                {
                    k = 10;
                    playerManager.onTutoScreen = true;
                    inventory.colaLionQty += 1;
                }

                else if(consumableData.consumableName == "Katorro")
                {
                    k = 11;
                    playerManager.onTutoScreen = true;
                    inventory.katorroQty += 1;
                }

                else if(consumableData.consumableName == "Ikok")
                {
                    k = 12;
                    playerManager.onTutoScreen = true;
                    inventory.ikokQty += 1;
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

