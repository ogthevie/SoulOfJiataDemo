using System;
using System.Collections;
using UnityEngine;

public class BigKossiEventManager : EventStoryTriggerManager
{
    ArcLightEventManager arcLightEventManager;
    public GameObject altarArcLight, BigKossi, ExplosionFx;

    void Awake()
    {
        arcLightEventManager = FindObjectOfType<ArcLightEventManager>();
    }
    void Start()
    {

        StartCoroutine(HandleBigKossiFlame());
        
        
        IEnumerator HandleBigKossiFlame()
        {
            for(int k = 0; k < 8; k++)
            {
                arcLightEventManager.Torche[k].transform.parent.GetChild(1).gameObject.SetActive(true);
                yield return new WaitForSeconds(2.5f);
            }

            yield return new WaitForSeconds(3f);
            
            ExplosionFx.SetActive(true);
            Destroy(altarArcLight, 0.5f);
            BigKossi.SetActive(true);

        }
    }
    protected override void OnCollisionEnter(Collision other)
    {
        throw new NotImplementedException();
    }
}
