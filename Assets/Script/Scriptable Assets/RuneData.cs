using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Jiata/Rune Data")]

[System.Serializable]
public class RuneData : ScriptableObject
{
    public bool base_Door, mid_Door, sup_Door;
    public bool base_DoorB;
    public bool mid_DoorH, mid_DoorB;
    public bool sup_DoorH, sup_DoorG, sup_DoorB;
}
