using UnityEngine;

public class NgomaaManager : CharacterManager
{
    [SerializeField] GameObject fireCook, smokeCook;
    protected override void Start()
    {
        base.Start();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
        if(dayPeriod > 1) 
        {
            fireCook.SetActive(false);
            smokeCook.SetActive(false);
        }
    }

}

