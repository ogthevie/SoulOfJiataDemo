using UnityEngine;

[CreateAssetMenu(fileName = "Sorcery", menuName = "Jiata/Sourcery Data")]
public class SorceryData : ScriptableObject
{
    public string sorceryName;
    public Sprite sorcerySprite;
    public float timer;
    [TextArea(1,6)]public string sorceryDescription;
    public bool isActive;
}
