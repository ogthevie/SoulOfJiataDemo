using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SJ;

[DefaultExecutionOrder(-3)]
public class GameSaveManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] PlayerStats playerStats;
    StoryManager storyManager;
    GameManager gameManager;
    [SerializeField] ArcLightEventManager arcLightEventManager;
    public GameObject baseDoor, midDoor, supDoor;
    public Transform baseDoorPosition, midDoorPosition;
    public bool isloaded;

    void Awake()
    {

        string ActiveScene = SceneManager.GetActiveScene().name;
        //playerManager = FindObjectOfType<PlayerManager>();
        //playerStats = FindObjectOfType<PlayerStats>();
        gameManager = GetComponent<GameManager>();
        storyManager = GetComponent<StoryManager>();

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

        /*if(ActiveScene == "GrotteKossi")
        {
            arcLightEventManager = FindFirstObjectByType<ArcLightEventManager>();
            baseDoor = GameObject.Find("Base Door");
            midDoor = GameObject.Find("MidDoorDown");
            baseDoorPosition = baseDoor.transform;
            midDoorPosition = midDoor.transform;
        }*/


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
        SavePlayerPosition();
        SaveStoryData();
        Debug.Log("sauvegarde effectuée");
    }

    public void LoadAllData()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        LoadStoryData();
        LoadPlayerData();
        Debug.Log("Données chargées");
        isloaded = true;
        
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
        Debug.Log("portes save");
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
        };
        string playerJSon = JsonUtility.ToJson(playerData);

        string filepath = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(filepath, playerJSon);
    }

    void SavePlayerPosition()
    {
        PlayerPosition playerPosition = new PlayerPosition
        {
            playerX = playerManager.gameObject.transform.position.x,
            playerY = playerManager.gameObject.transform.position.y,
            playerZ = playerManager.gameObject.transform.position.z
        };
        string playerJSon = JsonUtility.ToJson(playerPosition);

        //Debug.Log(playerJSon);

        string filepath = Application.persistentDataPath + "/playerPosition.json";
        System.IO.File.WriteAllText(filepath, playerJSon);
    }

    void SaveStoryData()
    {
        StoryData storyData = new StoryData
        {
            storyStep = storyManager.storyStep,
            activeScene = SceneManager.GetActiveScene().name
        };

        string storyStepJson = JsonUtility.ToJson(storyData);

        string filepath = Application.persistentDataPath + "/StoryData.json";
        System.IO.File.WriteAllText(filepath, storyStepJson); 
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

            //if(baseDoor.transform.GetChild(1).TryGetComponent<RuneManager>(out RuneManager component)) component.LoadStateBaseRune();
            if(!baseDoor.TryGetComponent<DoorManager>(out DoorManager component)) 
            {
                if(baseDoor.transform.GetChild(1).TryGetComponent<RuneManager>(out RuneManager component1)) component1.LoadStateBaseRune();
            }
        }
        else
        {
            Debug.Log("Aucune porte trouvée");
        }
    }

    public void LoadTorcheGrotteData()
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
 }

    public void LoadPlayerPosition()
    {
        string filePath = Application.persistentDataPath + "/playerPosition.json";

        if(System.IO.File.Exists(filePath))
        {
            string playerJson = System.IO.File.ReadAllText(filePath);
            PlayerPosition playerPosition = JsonUtility.FromJson<PlayerPosition>(playerJson);

            playerManager.gameObject.transform.position = new Vector3(playerPosition.playerX, playerPosition.playerY, playerPosition.playerZ);
        }

        Debug.Log("Position chargée");
  }

    void LoadStoryData()
    {
        string filepath = Application.persistentDataPath + "/StoryData.json";
        string sceneName;

        if(System.IO.File.Exists(filepath))
        {
            string storyStepJson = System.IO.File.ReadAllText(filepath);
            StoryData storyData = JsonUtility.FromJson<StoryData>(storyStepJson);

            storyManager.storyStep = storyData.storyStep;
            sceneName = storyData.activeScene;
            StartCoroutine(StartLoadingScene());
        }

        IEnumerator StartLoadingScene()
        {     
            gameManager.loadSlider.fillAmount = 0;
            gameManager.loadingScreen.enabled = true;
            yield return new WaitForSeconds(0.15f);
            gameManager.LoadScene(sceneName);
        }
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

        isloaded = false;
    }

    public void HandleGrotteKossiDoor()
    {
        string ActiveScene = SceneManager.GetActiveScene().name;
        if(ActiveScene == "GrotteKossi")
        {
            arcLightEventManager = FindFirstObjectByType<ArcLightEventManager>();
            baseDoor = GameObject.Find("Base Door");
            midDoor = GameObject.Find("MidDoorDown");

            baseDoorPosition = baseDoor.transform;
            midDoorPosition = midDoor.transform;
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
    public int playerPV;
    public bool surcharge, arcLight, thunder, canBaemb;
}

class PlayerPosition
{
    public float playerX, playerY, playerZ;
}

class TorcheData
{
    public int [] HeartStelesState = new int[8];
}

class StoryData
{
    public int storyStep;
    public string activeScene;
}



#endregion