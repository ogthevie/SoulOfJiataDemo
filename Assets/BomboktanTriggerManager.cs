using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomboktanTriggerManager : DialogTriggerManager
{
    public override void OnTriggerEnter(Collider other)
    {
        if(storyManager.storyStep == 5)
        {
            idDialog = 0;
        }
        else if(storyManager.storyStep == 7)
        {
            idDialog = 1;
        }

        base.OnTriggerEnter(other);        
    }    
    /// Introduction à la grotte + discours sur les bracelets
    /// Don du sortilège du sommeil de l'autre monde
    /// presentation avec BigKossi *apres le combat
}
