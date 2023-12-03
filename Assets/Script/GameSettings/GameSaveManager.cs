using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using SJ;

[DefaultExecutionOrder(-3)]
public class GameSaveManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerStats playerStats;
    ArcLightEventManager arcLightEventManager;
    public GameObject baseDoor, midDoor, supDoor;
    public Transform baseDoorPosition, midDoorPosition;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        arcLightEventManager = FindFirstObjectByType<ArcLightEventManager>();

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

        baseDoorPosition = baseDoor.transform;
        midDoorPosition = midDoor.transform;

    }

    void Start()
    {
        LoadAllData();
    }

    public void SaveAllData()
    {
        int i = SceneManager.GetActiveScene().buildIndex;

        if(i == 2) 
        {
            SaveDoorData();
            SaveTorcheGrotteData();
        }

        SavePlayerData();
        Debug.Log("sauvegarde effectuée");
    }

    public void LoadAllData()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        if(i == 2) 
        {
            LoadGrotteData();
            LoadTorcheGrotteData();
        }
        LoadPlayerData();
        Debug.Log("Données chargées");
    }

    #region Sauvegarde

    public void SaveDoorData()
    {
        GrotteKossiData grotteKossiData = new GrotteKossiData
        {
            basedoorX = baseDoorPosition.position.x, basedoorY = baseDoorPosition.position.y, baseDoorZ = baseDoorPosition.position.z,
            midDoorX = midDoorPosition.position.x, midDoorY = midDoorPosition.position.y, midDoorZ = midDoorPosition.position.z,
        };

        string grotteKossiJon = JsonUtility.ToJson(grotteKossiData);
        string filePath = Application.persistentDataPath + "/grotteKossiData.json";
        System.IO.File.WriteAllText(filePath, grotteKossiJon);
    }

    void SavePlayerData()
    {
        PlayerData playerData = new PlayerData
        {
            playerPV = playerStats.currentHealth, 
            surcharge = playerManager.canSurcharge, 
            arcLight = playerManager.canArcLight, 
            thunder = playerManager.canThunder,
            canBaemb = playerManager.canBaemb,
            playerX = playerManager.gameObject.transform.position.x,
            playerY = playerManager.gameObject.transform.position.y,
            playerZ = playerManager.gameObject.transform.position.z,
        };

        string playerJSon = JsonUtility.ToJson(playerData);
        string filepath = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(filepath, playerJSon);
    }

    void SaveTorcheGrotteData()
    {
        TorcheData torcheData = new TorcheData();
        for(int i = 0; i < 8; i++)
        {
            torcheData.HeartStelesState[i] = arcLightEventManager.IndexHeartSteles[i];
        }

        string torcheJson = JsonUtility.ToJson(torcheData);
        string filepath = Application.persistentDataPath + "/torcheData.json";
        System.IO.File.WriteAllText(filepath, torcheJson);

    }


    #endregion
    
    #region chargement des données
    void LoadGrotteData()
    {
        string filePath = Application.persistentDataPath + "/grotteKossiData.json";

        if (System.IO.File.Exists(filePath))
        {
            string grotteKossiJson = System.IO.File.ReadAllText(filePath);
            GrotteKossiData grotteKossiData = JsonUtility.FromJson<GrotteKossiData>(grotteKossiJson);

            // Appliquer la position chargée au GameObject
            baseDoorPosition.position = new Vector3(grotteKossiData.basedoorX, grotteKossiData.basedoorY, grotteKossiData.baseDoorZ);
            midDoorPosition.position = new Vector3(grotteKossiData.midDoorX, grotteKossiData.midDoorY, grotteKossiData.midDoorZ);

            if(baseDoor.transform.GetChild(1).TryGetComponent<RuneManager>(out RuneManager component))
                component.LoadStateBaseRune();

        }
        else
        {
            Debug.Log("Aucune porte trouvée");
        }
    }

    void LoadTorcheGrotteData()
    {
        string filepath = Application.persistentDataPath + "/torcheData.json";

        if(System.IO.File.Exists(filepath))
        {
            string torcheJson = System.IO.File.ReadAllText(filepath);
            TorcheData torcheData = JsonUtility.FromJson<TorcheData>(torcheJson);

            for(int i = 0; i < 8; i++)
            {
                arcLightEventManager.IndexHeartSteles[i] = torcheData.HeartStelesState[i];
            }
        }
    }

    void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";

        if(System.IO.File.Exists(filePath))
        {
            string playerJson = System.IO.File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerJson);

            playerManager.gameObject.transform.position = new Vector3(playerData.playerX, playerData.playerY, playerData.playerZ);

            playerStats.healthBar.SetCurrentHealth(playerData.playerPV);
            playerStats.currentHealth = playerData.playerPV;

            playerManager.canSurcharge = playerData.surcharge;
            playerManager.canArcLight = playerData.arcLight;
            playerManager.canThunder = playerData.thunder;
            playerManager.canBaemb = playerData.canBaemb;
            playerManager.HandleSurchargeBrassard();
        }

        int i = SceneManager.GetActiveScene().buildIndex;
        if(i == 2) FindObjectOfType<SpawnPlayer>().gameObject.SetActive(false);
        //verifier si nous sommes dans la grotte
        //si nous sommes dans la grotte, desactiver le startZoneKossi, sinon laisser activer

        //verifier si nous sommes dans le village de bongo
        // si nous sommes dans le village, desactiver le startZoneSibongo et  le PlayerInitPosition, sinon laisser activer
    }

    #endregion
    
    public void ClearAllSaves()
    {
        // Chemin du dossier de sauvegarde
        string saveFolderPath = Application.persistentDataPath;

        try
        {
            // Récupère tous les fichiers JSON dans le dossier de sauvegarde
            string[] saveFiles = System.IO.Directory.GetFiles(saveFolderPath, "*.json");

            // Supprime chaque fichier
            foreach (string file in saveFiles)
            {
                System.IO.File.Delete(file);
                Debug.Log("Fichier supprimé : " + file);
            }

            Debug.Log("Toutes les sauvegardes ont été effacées.");
        }
        catch (Exception e)
        {
            Debug.LogError("Erreur lors de la suppression des sauvegardes : " + e.Message);
        }
    }

}

#region  classe de data à save
class GrotteKossiData
{
    public float basedoorX, basedoorY, baseDoorZ;
    public float midDoorX, midDoorY, midDoorZ;
}

class PlayerData
{
    public float playerX, playerY, playerZ;
    public int playerPV;
    public bool surcharge, arcLight, thunder, canBaemb;
}

class TorcheData
{
    public int [] HeartStelesState = new int[8];
}



#endregion