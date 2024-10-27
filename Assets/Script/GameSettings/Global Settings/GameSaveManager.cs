using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SJ;

[DefaultExecutionOrder(-3)]
public class GameSaveManager : MonoBehaviour
{
    public PlayerManager playerManager;
    [SerializeField] PlayerStats playerStats;
    StoryManager storyManager;
    GameManager gameManager;
    [SerializeField] ThunderEventManager thunderEventManager;
    [SerializeField] GameObject saveUi;
    [SerializeField] InventoryData inventoryData;
    public bool isloaded;

    void Awake()
    {

        string ActiveScene = SceneManager.GetActiveScene().name;
        //playerManager = FindFirstObjectByType<PlayerManager>();
        //playerStats = FindFirstObjectByType<PlayerStats>();
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
    }

    public void SaveAllData()
    {
        int i = SceneManager.GetActiveScene().buildIndex;

        if(i == 2) SaveSteleGrotteData();

        SavePlayerData();
        SavePlayerPosition();
        SaveStoryData();
        SaveTutoData();
        Debug.Log("sauvegarde effectuée");
        StartCoroutine (HandleSaveUI());
    }

    IEnumerator HandleSaveUI()
    {
        saveUi.SetActive(true);
        yield return new WaitForSeconds(3f);
        saveUi.SetActive(false);
    }

    #region Sauvegarde

    void SavePlayerData()
    {
        PlayerData playerData = new PlayerData
        {
            playerPV = playerStats.currentHealth, 
            gauntlet = playerManager.haveGauntlet, 
            arcLight = playerManager.canArcLight, 
            thunder = playerManager.canThunder,
            canBaemb = playerManager.canBaemb,
            canSomm = playerManager.canSomm,
            canSurcharge = playerManager.canSurcharge,
            ikokQty = inventoryData.ikokQty,
            selQty = inventoryData.selQty,
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

    void SaveSteleGrotteData()
    {
        SteleData steleData = new SteleData();

        GrotteKossiManager grotteKossiManager = FindFirstObjectByType<GrotteKossiManager>();

        for(int i = 0; i < 3; i++)
        {
            steleData.HeartStelesState[i] = grotteKossiManager.steleManagers[i].state;
        }

        string steleJson = JsonUtility.ToJson(steleData);
        string filepath = Application.persistentDataPath + "/steleData.json";
        System.IO.File.WriteAllText(filepath, steleJson);

    }

    void SaveTutoData()
    {
        TutoManager tutoManager = FindFirstObjectByType<TutoManager>();
        TutoData tutoData = new TutoData
        {
            dialogT = tutoManager.dialogTuto,
            paralyzeT = tutoManager.paralyzeTuto,
            arcLightT = tutoManager.arcLightTuto,
            thunderT = tutoManager.thunderTuto
        };

        string tutoDataJson = JsonUtility.ToJson(tutoData);

        string filePath = Application.persistentDataPath + "/TutoData.json";
        System.IO.File.WriteAllText(filePath, tutoDataJson);

    }


    #endregion
    
    #region chargement des données

    public void LoadAllData()
    {
        LoadStoryData();
        LoadPlayerData();
        LoadTutoData();
        playerManager.isDead = false;
        playerStats.stateJiataData.isHidden = false;
        Debug.Log("Toutes les données du jeu ont été chargées");
        isloaded = true;
    }

    public void LoadSteleGrotteData()
    {
        string filepath = Application.persistentDataPath + "/steleData.json";

        if(System.IO.File.Exists(filepath))
        {
            string steleJson = System.IO.File.ReadAllText(filepath);
            SteleData steleData = JsonUtility.FromJson<SteleData>(steleJson);

            GrotteKossiManager grotteKossiManager = FindFirstObjectByType<GrotteKossiManager>();

            for(int i = 0; i < 3; i++)
            {
                grotteKossiManager.steleManagers[i].state = steleData.HeartStelesState[i];
                if(grotteKossiManager.steleManagers[i].state == 1) grotteKossiManager.steleManagers[i].transform.GetChild(0).GetComponent<Renderer>().material = grotteKossiManager.steleManagers[i].lightingMaterial;
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

            playerManager.canSurcharge = playerData.canSurcharge;
            playerManager.haveGauntlet = playerData.gauntlet;
            playerManager.canArcLight = playerData.arcLight;
            playerManager.canThunder = playerData.thunder;
            playerManager.canBaemb = playerData.canBaemb;
            playerManager.canSomm = playerData.canSomm;
            inventoryData.ikokQty = playerData.ikokQty;
            inventoryData.selQty = playerData.selQty;
        }

        int i = SceneManager.GetActiveScene().buildIndex;
        if(i == 2) FindFirstObjectByType<SpawnPlayer>().gameObject.SetActive(false);
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

        Debug.Log("Position intialisée");
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

    void LoadTutoData()
    {
        string filepath = Application.persistentDataPath + "/TutoData.json";
        if(System.IO.File.Exists(filepath))
        {
            string tutoDataJson = System.IO.File.ReadAllText(filepath);
            TutoData tutoData = JsonUtility.FromJson<TutoData>(tutoDataJson);

            TutoManager tutoManager = FindFirstObjectByType<TutoManager>();

            tutoManager.dialogTuto = tutoData.dialogT;
            tutoManager.paralyzeTuto = tutoData.paralyzeT;
            tutoManager.arcLightTuto = tutoData.arcLightT;
            tutoManager.thunderTuto = tutoData.thunderT;
        }

        Debug.Log("Tutoriel chargée");
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

        inventoryData.ikokQty = inventoryData.selQty = 0;
    }

}

#region  classe de data à save
class PlayerData
{
    public int playerPV, ikokQty, selQty;
    public bool gauntlet, arcLight, thunder, canBaemb, canSomm, canSurcharge;
}

class PlayerPosition
{
    public float playerX, playerY, playerZ;
}

class SteleData
{
    public int [] HeartStelesState = new int[3];
}

class StoryData
{
    public int storyStep;
    public string activeScene;
}

class TutoData
{
    public bool dialogT, paralyzeT, arcLightT, thunderT;
}



#endregion