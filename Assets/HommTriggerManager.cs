using UnityEngine;

public class HommTriggerManager : DialogTriggerManager
{
    public override void OnTriggerEnter(Collider other)
    {
        if(storyManager.storyStep == 1) idDialog = 0;
        else if(storyManager.storyStep == 2) idDialog = 1;
        else if(storyManager.storyStep == 3) idDialog = 2;
        else if(storyManager.storyStep == 4) idDialog = 3;

        base.OnTriggerEnter(other);  
    }    
}
