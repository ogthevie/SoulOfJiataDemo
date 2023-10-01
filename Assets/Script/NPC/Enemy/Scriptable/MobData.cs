using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Jiata/Mob Data")]
public class MobData : ScriptableObject
{
    public string mobName;
    public int mobHealth;
    public GameObject mobPrefab;
    public int mobFAtt;
    public int mobSAtt;

}
