using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcLightEventManager : EventStoryTriggerManager
{
    public Material refMaterial, materialHeart;
    public GameObject [] HeartSteles = new GameObject [8];
    public GameObject [] Torche = new GameObject [8];


    void LateUpdate()
    {
        HandleTorcheLight();
    }
    private void HandleTorcheLight()
    {
        int i;
        for(i = 0; i <= 7; i++)
        {
            materialHeart = HeartSteles[i].GetComponent<Renderer>().sharedMaterial;
            if(materialHeart == refMaterial && !Torche[i].activeSelf) 
            {
                Torche[i].SetActive(true);
            }
            else if(materialHeart != refMaterial) Torche[i].SetActive(false);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        
    }
}
