public class LibumManager : CharacterManager
{
    protected override void Start()
    {
        base.Start();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
