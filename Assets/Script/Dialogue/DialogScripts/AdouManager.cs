using UnityEngine;

public class AdouManager : CharacterManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
