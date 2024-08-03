using UnityEngine;

public class NgomaaManager : CharacterManager
{
    [SerializeField] GameObject fireCook, smokeCook;
    protected override void OnEnable()
    {
        base.OnEnable();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
        if(dayPeriod > 1) 
        {
            fireCook.SetActive(false);
            smokeCook.SetActive(false);
        }
    }

}

