using UnityEngine;
using Random = UnityEngine.Random;
using SJ;

public class TreeContainerManager : MonoBehaviour
{
    public TreeContainerData treeContainerData;
    [SerializeField] PlayerUIManager playerUIManager;
    
    public float canProduceTimer;
    public bool canProduct;

    public int quantityConsumable;
    readonly float cycleTime = 10f; // a modifier, idealament xa doit etre 5 min donc 150f
    

    void Start()
    {
        canProduct = true;
        canProduceTimer = 0;
        quantityConsumable = 0;
        playerUIManager = FindObjectOfType<PlayerUIManager>();
    }

    void FixedUpdate()
    {
        float delta = Time.deltaTime;
        HandleProductionCycle(delta);       
    }

    public void HandleTreeContainerProcess()
    {
        if(!canProduct)
            return;

        if(treeContainerData.treeContainerName == "Nkomo Tree")
        {
            quantityConsumable = QuantityProductionNkomo();
            canProduct = false;
        }

        else if(treeContainerData.treeContainerName == "Prune Tree")
        {
            quantityConsumable = QuantityProductionPrune();
            canProduct = false;
        } 

        else if(treeContainerData.treeContainerName == "Mangue Tree")
        {
            quantityConsumable = QuantityProductionMangue();
            canProduct = false;
        } 
    
    }

    public void HandleProductionCycle(float delta)
    {
        if(!canProduct)
        {
            canProduceTimer += delta;
            if(canProduceTimer >= cycleTime)
            {
                canProduct = true;
                canProduceTimer = 0;
                quantityConsumable = 0;
            }
        }
    }

    static int QuantityProductionNkomo()
    {
        int quantity = Random.Range(1,6);
        return (quantity);
    }

    static int QuantityProductionPrune()
    {
        int quantity = Random.Range(1,4);
        return (quantity);
    }

    static int QuantityProductionMangue()
    {
        int quantity = Random.Range(1,4);
        return (quantity);
    }
}
