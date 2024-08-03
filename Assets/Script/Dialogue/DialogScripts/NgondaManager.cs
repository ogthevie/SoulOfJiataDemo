
public class NgondaManager : CharacterManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        DayJob(characterpositions[dayPeriod], characterRotation[dayPeriod]);
    }

}
