using UnityEngine;

public class HommManager : CharacterManager
{
    protected override void Start()
    {
        base.Start();
        dayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
