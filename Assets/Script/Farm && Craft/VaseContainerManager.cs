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

    public int qtyConsumable, qtyOne, qtyTwo;

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
            StartCoroutine(tutoManager.HandleToggleTipsUI("Ces vases sont des puits de vie, prêts à vous offrir un nouveau souffle"));
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
            bool canProductSel = CheckProbabilityConsumables();

            if(canProductSel)
            {
                qtyOne = QuantityProductionConsumable();
                qtyConsumable = qtyOne;
                int idConsumable = Random.Range(0,2);
                LoadConsumable(idConsumable);
            }
    }

    private void RandomiseConsumableGKumbo()
    {
        bool canProduceIkok = CheckProbabilityConsumables();

        if(canProduceIkok)
        {
            qtyTwo = QuantityProductionConsumable();
            qtyConsumable = qtyTwo;
            int idConsumable = Random.Range(0,2);
            LoadConsumable(idConsumable);
        }

    }

    static bool CheckProbabilityConsumables()
    {
        int probability = Random.Range(1,11);
        return (probability <= 4)? true : false;
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
