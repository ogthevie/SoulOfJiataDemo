using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKossiManager : CharacterManager
{
    protected override void Start()
    {
        dayPeriod = sibongoManager.dayPeriod;
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
        FixedCursorPosition();
    }
}
