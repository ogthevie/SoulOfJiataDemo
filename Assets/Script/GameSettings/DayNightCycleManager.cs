using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    /// 1 journée(24h) équivaut à 24 min donc 1h équivaut à 1 min (60s)
    /// 1080 c'est le debut de la nuit
    /// 300 - 720 c'est le matin
    /// 720 c'est le jour
    public int dayTimer;
    GameManager gameManager;

    void Awake()
    {
        InitialiseDayTimer();
    }

    public void InitialiseDayTimer()
    {
        gameManager = GetComponent<GameManager>();
        if(gameManager.newGame == 1) dayTimer = 0;
        else dayTimer = Random.Range(0,4);  //j'ai volontairement retiré la période ou tout le monde dort
    }

}
