using UnityEngine;
using DG.Tweening;
using SJ;

public class PlayerUIManager : MonoBehaviour
{

    public GameObject playerStatsUi, radarUI, radarBG;

    void Awake()
    {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Playerui"); // Recherche d'objets avec le tag spécifique

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

    void Start()
    {
        playerStatsUi = transform.GetChild(0).gameObject;
        radarBG = transform.GetChild(5).gameObject;
        radarUI = transform.GetChild(6).gameObject;
    }

    public void HiddenUI()
    {
        playerStatsUi.GetComponent<RectTransform>().DOAnchorPosX(-150, 0.4f, false);
        
        if(radarUI.activeSelf)
        {
            radarBG.GetComponent<RectTransform>().DOAnchorPosX(90, 0.4f, false);
            radarUI.GetComponent<RectTransform>().DOAnchorPosX(90, 0.4f, false);
        }
    }

    public void ShowUI()
    {
        playerStatsUi.GetComponent<RectTransform>().DOAnchorPosX(130, 0.4f, false);
        
        if(radarUI.activeSelf)
        {
            radarBG.GetComponent<RectTransform>().DOAnchorPosX(-90, 0.4f, false);
            radarUI.GetComponent<RectTransform>().DOAnchorPosX(-90, 0.4f, false);
        }
    }
}
