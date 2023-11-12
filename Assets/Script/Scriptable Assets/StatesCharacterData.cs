using UnityEngine;

[CreateAssetMenu (fileName = "States Character", menuName = "Jiata/States Character Data")]
public class StatesCharacterData : ScriptableObject
{
    public bool isAttacking;
    public bool isInteract;
    public bool isIndomitable;
    public bool isHidden;
    public int d_HighAttack = 10;
    public int d_LowAttack = 5;
    public int d_ArcLight = 50;
    public int d_Thunder = 160;
    public int d_Magneti = 5;

}
