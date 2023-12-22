using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NgondaManager : CharacterManager
{
    protected override void Start()
    {
        base.Start();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
