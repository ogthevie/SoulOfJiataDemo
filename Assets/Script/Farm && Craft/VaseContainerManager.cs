using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SJ;

public class VaseContainerManager : MonoBehaviour
{
    public VaseContainerData vaseContainerData;
    TutoManager tutoManager;
    AudioManager audioManager;
    public List<ConsumableData> consumableDatas = new ();
    public GameObject destroyFx;

    public int qtyConsumable, qtyMintoumba, qtySel, qtyColaS, qtyColaL, qtyIkok;

    void Awake()
    {
        tutoManager = FindObjectOfType<TutoManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void HandleVaseConatinerProcess()
    {
        StartCoroutine(HandleVaseContainer());
        if(!tutoManager.vasetuto)
        {
            StartCoroutine(tutoManager.HandleToggleTipsUI("Les vases contiennent des denrées qui vous feront gagner soit en vitalité soit en énergie, ou les deux"));
            tutoManager.vasetuto = true;
        }
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
            bool canProductSel = CheckProbabilityConsumables();

            if(canProductMintoumba)
            {
                qtyMintoumba = QuantityProductionConsumable();
                qtyConsumable = qtyMintoumba;
                LoadConsumable(0);
            }

            if(canProductSel)
            {
                qtySel = QuantityProductionConsumable();
                qtyConsumable = qtySel;
                LoadConsumable(1);
            }
    }

    private void RandomiseConsumableGKumbo()
    {
        bool canProduceColaS = CheckProbabilityConsumables();
        bool canProduceColaL = CheckProbabilityConsumables();
        bool canProduceIkok = CheckProbabilityConsumables();
        
        if(canProduceColaS)
        {
            qtyColaS = QuantityProductionConsumable();
            qtyConsumable = qtyColaS;
            LoadConsumable(2);
        }
        if(canProduceColaL)
        {
            qtyColaL = QuantityProductionConsumable();
            qtyConsumable = qtyColaL;
            LoadConsumable(3);
        }
        if(canProduceIkok)
        {
            qtyIkok = QuantityProductionConsumable();
            qtyConsumable = qtyIkok;
            LoadConsumable(4);
        }

    }

    static bool CheckProbabilityConsumables()
    {
        int probability = Random.Range(1,11);
        return (probability <= 5)? true : false;
    }

    static int QuantityProductionConsumable()
    {
        int quantity = Random.Range(1,3);
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
