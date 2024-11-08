using UnityEngine;
using DG.Tweening;
using TMPro;
using SJ;

public class PlayerUIManager : MonoBehaviour
{

    public GameObject playerStatsUi, padUI, sorceryUI, interactionUI;
    GameManager gameManager;

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
        gameManager = FindFirstObjectByType<GameManager>();
        transform.GetChild(1).gameObject.SetActive(false);
        interactionUI.SetActive(false);
        if(gameManager.newGame == 1)
        {
            InventoryManager inventoryManager = padUI.GetComponent<InventoryManager>();
            inventoryManager.inventory.selQty = inventoryManager.inventory.ikokQty = 0;
            inventoryManager.HandleItemsQty();
        }
    }

    public void HiddenUI()
    {
        playerStatsUi.GetComponent<RectTransform>().DOAnchorPosX(-800f, 0.4f, false);
        padUI.GetComponent<RectTransform>().DOAnchorPosX(250f, 0.4f, false);
        sorceryUI.GetComponent<RectTransform>().DOAnchorPosX(-200, 0.4f, false);
    }

    public void ShowUI()
    {
        playerStatsUi.GetComponent<RectTransform>().DOAnchorPosX(-1f, 0.4f, false);
        padUI.GetComponent<RectTransform>().DOAnchorPosX(-100f, 0.4f, false);
        sorceryUI.GetComponent<RectTransform>().DOAnchorPosX(200, 0.4f, false);
    }

    public void ShowInteractionUI(string action)
    {
        interactionUI.SetActive(true);
        interactionUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = action;
    }

    public void HiddenInteractionUI()
    {
        interactionUI.SetActive(false);
    }
}
