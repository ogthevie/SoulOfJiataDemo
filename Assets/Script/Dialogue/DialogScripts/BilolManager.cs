using System;
using UnityEngine;

public class BilolManager : CharacterManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
        Debug.Log(dayPeriod);
    }

}
