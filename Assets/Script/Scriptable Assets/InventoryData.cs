using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Jiata/Inventory Data")]

[System.Serializable]
public class InventoryData : ScriptableObject

{
    public int nkomoQty;
    public int pruneQty;
    public int mangueQty;
    public int mintoumbaQty;
    public int matangoQty;
    public int gesierQty;
    public int kalabaQty;
    public int odontolQty;
    public int colaSingeQty;
    public int colaLionQty;
    public int katorroQty;
    public int ikokQty;

    public bool haveKatorro, haveDentBK, havePierreSel, haveJujube, havePierreBaemb, haveCorde, haveEauCad, haveClouMoluk;
    public bool canBaemb, canSomm, canDest, canPur;
}
