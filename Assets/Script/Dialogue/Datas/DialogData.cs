using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Jiata/Dialog Data")]
public class DialogData : ScriptableObject
{
    public string characterName;
    public string mainCharacterName = "Ruben";
    [TextArea(1,3)] public List <string> firstConversation = new List<string>();
    [TextArea(1,3)] public List <string> secondConversation = new List<string>();
    [TextArea(1,3)] public List <string> thirdConversation = new List<string>();

    public bool fConv, sConv;

}
