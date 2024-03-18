using System;
using System.Collections;
using SJ;
using UnityEngine;

public class BigKossiEventManager : EventStoryTriggerManager
{
    ArcLightEventManager arcLightEventManager;
    [SerializeField] GameObject explosionFx;

    void Awake()
    {
        cameraShake = FindObjectOfType<PlayerLocomotion>().cameraManager.GetComponent<CameraShake>();
        arcLightEventManager = FindObjectOfType<ArcLightEventManager>();
    }

    void Start()
    {
        //cameraShake.Shake(7, 0.5f);
        //explosionFx.SetActive(true);
    }

    protected override void OnCollisionEnter(Collision other)
    {

    }
}
