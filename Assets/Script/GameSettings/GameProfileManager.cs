using UnityEngine;

public class GameProfileManager : MonoBehaviour
{
    public InventoryData inventoryData;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager"); // Recherche d'objets avec le tag spécifique

        if (objs.Length > 1) // Vérifier s'il y a déjà des objets persistants présents
        {
            // Si oui, détruire les doublons
            for (int i = 1; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
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
