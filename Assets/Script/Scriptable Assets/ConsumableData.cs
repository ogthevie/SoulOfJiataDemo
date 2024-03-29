using UnityEngine;


[CreateAssetMenu(fileName = "Consumable", menuName = "Jiata/Consumable Data")]
public class ConsumableData : ScriptableObject
{

    public Sprite consumablesprite;
    public Sprite consumableIcon;
    public string consumableName;
    public int HealthPoint;
    public int StaminaPoint;
    public GameObject consumablePrefab;

}
