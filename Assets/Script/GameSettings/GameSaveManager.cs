using System;
using UnityEngine;
using SJ;

public class GameSaveManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerStats playerStats;
    public GameObject baseDoor, midDoor, supDoor, msOne, msTwo;
    public Transform baseDoorPosition, midDoorPosition, supDoorPosition, msOnePos, msTwoPos;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStats = FindObjectOfType<PlayerStats>();

        CollectMagnetSphere();

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
        supDoorPosition = supDoor.transform;
        msOnePos = msOne.transform;
        msTwoPos = msTwo.transform;

    }

    void Start()
    {
        //LoadAllData();
    }

    void CollectMagnetSphere()
    {
        MagnetSphereManager[] magnetSpheres = FindObjectsOfType<MagnetSphereManager>();

        msOne = magnetSpheres[0].gameObject;
        msTwo = magnetSpheres[1].gameObject;
    }

    public void SaveAllData()
    {
        SaveGrotteData();
        SavePlayerData();
        Debug.Log("sauvegarde effectuée");
    }

    public void LoadAllData()
    {
        LoadGrotteData();
        LoadPlayerData();
    }

    public void SaveGrotteData()
    {
        CollectMagnetSphere();

        GrotteKossiData grotteKossiData = new GrotteKossiData
        {
            basedoorX = baseDoorPosition.position.x, basedoorY = baseDoorPosition.position.y, baseDoorZ = baseDoorPosition.position.z,
            midDoorX = midDoorPosition.position.x, midDoorY = midDoorPosition.position.y, midDoorZ = midDoorPosition.position.z,
            supDoorX = supDoorPosition.position.x, supDoorY = midDoorPosition.position.y, supDoorZ = supDoorPosition.position.z,

            msOneX = msOnePos.position.x, msOneY = msOnePos.position.y, msOneZ = msOnePos.position.z,
            msTwoX = msTwoPos.position.x, msTwoY = msTwoPos.position.y, msTwoZ = msTwoPos.position.z,
        };

        string grotteKossiJon = JsonUtility.ToJson(grotteKossiData);
        string filePath = Application.persistentDataPath + "/grotteKossiData.json";
        System.IO.File.WriteAllText(filePath, grotteKossiJon);
    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData
        {
            playerPV = playerStats.currentHealth, surcharge = playerManager.canSurcharge, arcLight = playerManager.canArcLight, thunder = playerManager.canThunder
        };

        string playerJSon = JsonUtility.ToJson(playerData);
        string filepath = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(filepath, playerJSon);
    }

    public void LoadGrotteData()
    {
        string filePath = Application.persistentDataPath + "/grotteKossiData.json";

        if (System.IO.File.Exists(filePath))
        {
            string grotteKossiJson = System.IO.File.ReadAllText(filePath);
            GrotteKossiData grotteKossiData = JsonUtility.FromJson<GrotteKossiData>(grotteKossiJson);

            // Appliquer la position chargée au GameObject
            baseDoorPosition.position = new Vector3(grotteKossiData.basedoorX, grotteKossiData.basedoorY, grotteKossiData.baseDoorZ);
            midDoorPosition.position = new Vector3(grotteKossiData.midDoorX, grotteKossiData.midDoorY, grotteKossiData.midDoorZ);
            supDoorPosition.position = new Vector3(grotteKossiData.supDoorX, grotteKossiData.supDoorY, grotteKossiData.supDoorZ);

            msOnePos.position = new Vector3(grotteKossiData.msOneX, grotteKossiData.msOneY, grotteKossiData.msOneZ);
            msTwoPos.position = new Vector3(grotteKossiData.msTwoX, grotteKossiData.msTwoY, grotteKossiData.msTwoZ);

            if(baseDoor.transform.GetChild(1).TryGetComponent<RuneManager>(out RuneManager component))
                component.LoadStateBaseRune();

        }
        else
        {
            Debug.Log("Aucune position sauvegardée trouvée");
        }
    }

    public void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";

        if(System.IO.File.Exists(filePath))
        {
            string playerJson = System.IO.File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerJson);

            playerStats.healthBar.SetCurrentHealth(playerData.playerPV);
            playerStats.currentHealth = playerData.playerPV;

            playerManager.canSurcharge = playerData.surcharge;
            playerManager.canArcLight = playerData.arcLight;
            playerManager.canThunder = playerData.thunder;
            playerManager.HandleSurchargeBrassard();
        }
    }

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

public class GrotteKossiData
{
    public float basedoorX, basedoorY, baseDoorZ;
    public float midDoorX, midDoorY, midDoorZ;
    public float supDoorX, supDoorY, supDoorZ;
    public float msOneX, msOneY, msOneZ;
    public float msTwoX, msTwoY, msTwoZ;
}

public class PlayerData
{
    public int playerPV;
    public bool surcharge, arcLight, thunder;
}