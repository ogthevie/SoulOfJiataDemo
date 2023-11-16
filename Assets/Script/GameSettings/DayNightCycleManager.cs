using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    public float dayTimer;
    
    void Update()
    {
        float delta = Time.deltaTime;
        HandleDayTimer(delta);
    }

    void HandleDayTimer(float delta)
    {
        if(dayTimer > 1440f)    dayTimer = 0;
        dayTimer += delta;
    }

}
