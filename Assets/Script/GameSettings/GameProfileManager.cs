using UnityEngine;

public class GameProfileManager : MonoBehaviour
{
    public InventoryData inventoryData;

    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.H)) SaveData();
        if(Input.GetKey(KeyCode.J)) LoadData();
    }

    public void SaveData()
    {
        string inventory = JsonUtility.ToJson(inventoryData);
        string filePath = Application.persistentDataPath + "/inventory.Json";
        System.IO.File.WriteAllText (filePath, inventory);
        Debug.Log("sauvegarde effectuée");
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/inventory.Json";
        string inventory = System.IO.File.ReadAllText(filePath);

        JsonUtility.FromJsonOverwrite(inventory, inventoryData);
    }


}
