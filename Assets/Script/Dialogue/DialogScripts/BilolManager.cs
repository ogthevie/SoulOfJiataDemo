public class BilolManager : CharacterManager
{
    protected override void Start()
    {
        base.Start();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
