using System;
using System.Collections;
using UnityEngine;

public class BigKossiEventManager : EventStoryTriggerManager
{
    ArcLightEventManager arcLightEventManager;
    [SerializeField] GameObject explosionFx;

    void Awake()
    {
        arcLightEventManager = FindObjectOfType<ArcLightEventManager>();
    }

    void Start()
    {
        explosionFx.SetActive(true);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        throw new NotImplementedException();
    }
}
