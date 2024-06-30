using System;
using System.Collections;
using SJ;
using UnityEngine;

public class BigKossiEventManager : EventStoryTriggerManager
{
    ThunderEventManager thunderEventManager;
    [SerializeField] GameObject explosionFx;

    protected override void OnTriggerEnter(Collider other)
    {

    }
}
