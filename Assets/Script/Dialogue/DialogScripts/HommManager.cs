public class HommManager : CharacterManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }
}
