using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKossiManager : CharacterManager
{
    protected override void OnEnable()
    {
        dayPeriod = sibongoManager.dayPeriod;
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
        FixedCursorPosition();
    }

    public override void FixedCursorPosition()
    {
        base.FixedCursorPosition();
        if(storyManager.storyStep == 2)
        {
            GameManager gameManager = storyManager.GetComponent<GameManager>();
            StartCoroutine(gameManager.StartHandleToDo(storyManager.storyStep));
        }
    }
}
