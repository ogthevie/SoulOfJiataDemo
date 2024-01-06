using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class VaseContainerManager : MonoBehaviour
{
    public VaseContainerData vaseContainerData;
    AudioManager audioManager;
    public List<ConsumableData> consumableDatas = new ();
    public GameObject destroyFx;

    public int qtyConsumable, qtyMintoumba, qtyMatango, qtyGesier, qtyKalaba, qtyColaS, qtyColaL, qtyOdon;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void HandleVaseConatinerProcess()
    {
        StartCoroutine(HandleVaseContainer());
    }

    IEnumerator HandleVaseContainer()
    {
        destroyFx.GetComponent<ParticleSystem>().Play();

        if(vaseContainerData.vaseContainerName == "PKumbo Black" || vaseContainerData.vaseContainerName == "PKumbo White")
            RandomiseConsumablePKumbo();
        else if(vaseContainerData.vaseContainerName == "GKumbo Black" || vaseContainerData.vaseContainerName == "GKumbo White")
        {
            RandomiseConsumablePKumbo();
            RandomiseConsumableGKumbo();
        }

        audioManager.BreakVase();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

    private void RandomiseConsumablePKumbo()
    {
            bool canProductMintoumba = CheckProbabilityConsumables();
            bool canProductMatango = CheckProbabilityConsumables();
            bool canProductGesier = CheckProbabilityConsumables();
            bool canProductKalaba = CheckProbabilityConsumables();

            if(canProductMintoumba)
            {
                qtyMintoumba = QuantityProductionConsumable();
                qtyConsumable = qtyMintoumba;
                LoadConsumable(0);
            }

            if(canProductMatango)
            {
                qtyMatango = QuantityProductionConsumable();
                qtyConsumable = qtyMatango;
                LoadConsumable(1);
            }

            if(canProductGesier)
            {
                qtyGesier = QuantityProductionConsumable();
                qtyConsumable = qtyGesier;
                LoadConsumable(2);
            }

            if(canProductKalaba)
            {
                qtyKalaba = QuantityProductionConsumable();
                qtyConsumable = qtyGesier;
                LoadConsumable(3);
            }
    }

    private void RandomiseConsumableGKumbo()
    {
        bool canProduceColaS = CheckProbabilityConsumables();
        bool canProduceColaL = CheckProbabilityConsumables();
        bool canProduceOdon = CheckProbabilityConsumables();
        
        if(canProduceColaS)
        {
            qtyColaS = QuantityProductionConsumable();
            qtyConsumable = qtyColaS;
            LoadConsumable(4);
        }
        if(canProduceColaL)
        {
            qtyColaL = QuantityProductionConsumable();
            qtyConsumable = qtyColaS;
            LoadConsumable(5);
        }
        if(canProduceOdon)
        {
            qtyOdon = QuantityProductionConsumable();
            qtyConsumable = qtyOdon;
            LoadConsumable(6);
        }

    }

    static bool CheckProbabilityConsumables()
    {
        int probability = Random.Range(1,11);
        return (probability <= 5)? true : false;
    }

    static int QuantityProductionConsumable()
    {
        int quantity = Random.Range(1,4);
        return(quantity);
    }

    private void LoadConsumable(int k)
    {
        if(gameObject.GetComponent<MeshRenderer>().enabled == false)
            return;
        for (int i = 0 ; i < qtyConsumable; i++)
        {
            GameObject visuals = Instantiate(consumableDatas[k].consumablePrefab);
            visuals.transform.SetParent(transform);
            visuals.transform.localPosition = Vector3.zero;
            visuals.transform.rotation = Quaternion.identity;
        }
    }
}
