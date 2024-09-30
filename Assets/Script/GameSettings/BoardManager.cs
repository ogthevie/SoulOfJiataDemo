using System.Collections;
using TMPro;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] GameObject boardUi, playerUi;
    [SerializeField] string description;
    [SerializeField] Color titleColor;
    
    void Awake()
    {
        playerUi = GameObject.Find("PlayerUI");
        boardUi = playerUi.transform.GetChild(8).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && !boardUi.activeSelf)
        {
            ShowDescription();
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3 && boardUi.activeSelf)
        {
            boardUi.SetActive(false);
        }        
    }

    void ShowDescription()
    {
        boardUi.SetActive(true);
        TextMeshProUGUI textMeshProUGUI =  boardUi.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.color = titleColor;
        textMeshProUGUI.text = description;
    }
}
