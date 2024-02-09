using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    /// 1 journée(24h) équivaut à 24 min donc 1h équivaut à 1 min (60s)
    /// 1080 c'est le debut de la nuit
    /// 300 - 720 c'est le matin
    /// 720 c'est le jour
    public float dayTimer;
    GameManager gameManager;

    void Awake()
    {
        InitialiseDayTimer();
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

    public void InitialiseDayTimer()
    {
        gameManager = GetComponent<GameManager>();
    if(gameManager.newGame == 1) dayTimer = 350;//dayTimer = 350;
        else
        {
            //dayTimer = Random.Range(360, 1120);
            dayTimer = 1122;
        }       
    }

}
