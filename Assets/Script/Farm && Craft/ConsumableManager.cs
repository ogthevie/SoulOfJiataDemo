using UnityEngine;

public class ConsumableManager : MonoBehaviour
{

    public ConsumableData consumableData;
    TreeContainerManager treeContainerManager;
    
    void Awake()
    {
        treeContainerManager = GetComponentInParent<TreeContainerManager>();
    }

    void FixedUpdate()
    {
        LoadConsumable();
    }
    
    public void LoadConsumable()
    {
        if(treeContainerManager.quantityConsumable > 0)
        {
            for(int i=0 ; i < treeContainerManager.quantityConsumable; i++)
            {
                GameObject visuals = Instantiate(consumableData.consumablePrefab);
                visuals.transform.SetParent(transform);
                visuals.transform.localPosition = Vector3.zero;
                visuals.transform.rotation = Quaternion.identity;
            }

            treeContainerManager.quantityConsumable = 0;

        }
    }

}
