using UnityEngine;

public class AdouManager : CharacterManager
{
    protected override void Start()
    {
        base.Start();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
