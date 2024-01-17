using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    /// 1 journée(24h) équivaut à 24 min donc 1h équivaut à 1 min (60s)
    /// 1080 c'est le debut de la nuit
    /// 300 - 720 c'est le matin
    /// 720 c'est le jour
    public float dayTimer;

    void Awake()
    {
        //dayTimer = Random.Range(360, 1120);
        dayTimer = 400;
    }
    
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
