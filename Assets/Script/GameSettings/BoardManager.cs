using System.Collections;
using TMPro;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] GameObject boardUi, playerUi;
    [SerializeField] string description;
    [SerializeField] Color titleColor;
    PlayerUIManager playerUIManager;
    
    void Awake()
    {
        playerUi = GameObject.Find("PlayerUI");
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        boardUi = playerUi.transform.GetChild(8).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && !boardUi.activeSelf)
        {
            StartCoroutine(ShowDescription());
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3 && boardUi.activeSelf)
        {
            boardUi.SetActive(false);
            playerUIManager.ShowUI();
        }        
    }

    IEnumerator ShowDescription()
    {
        playerUIManager.HiddenUI();
        boardUi.SetActive(true);
        TextMeshProUGUI textMeshProUGUI =  boardUi.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.color = titleColor;
        textMeshProUGUI.text = description;
        yield return new WaitForSeconds(4f);
        if(boardUi.activeSelf)
        {
            boardUi.SetActive(false);
            playerUIManager.ShowUI();
        }

    }
}
