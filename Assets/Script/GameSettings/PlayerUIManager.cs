using UnityEngine;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{

    public GameObject playerStatsUi, padUI;

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
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void HiddenUI()
    {
        playerStatsUi.GetComponent<RectTransform>().DOAnchorPosX(-400, 0.4f, false);
        padUI.GetComponent<RectTransform>().DOAnchorPosX(250f, 0.4f, false);
    }

    public void ShowUI()
    {
        playerStatsUi.GetComponent<RectTransform>().DOAnchorPosX(320, 0.4f, false);
        padUI.GetComponent<RectTransform>().DOAnchorPosX(-145, 0.4f, false);
    }
}
