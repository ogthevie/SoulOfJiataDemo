using UnityEngine;
using TMPro;
using SJ;

public class PauseMenuManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AudioManager audioManager;
    public GameObject inventory, buttonMap, iconInven, iconSorc, selector;
    public TextMeshProUGUI menuName;
    Vector3 selPos;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        audioManager = FindObjectOfType<AudioManager>();
        selector.transform.position = iconInven.transform.position;
        selPos = selector.transform.position;
    }

    void Update()
    {
        if(!playerManager.onPause)
            return;
        PrintMenu();
        HandleNavigationMenu();
        
    }



    private void HandleNavigationMenu() //Pensez à faire  une liste ici
    {
        if(inputManager.left_menu_input)
        {
            if(selPos == iconInven.transform.position)
            {
                selPos = iconSorc.transform.position;
                audioManager.NavigateMenuFx();
            }
            else if(selPos == iconSorc.transform.position)
            {
                selPos = iconInven.transform.position;
                audioManager.NavigateMenuFx();
            }
        }
        else if(inputManager.right_menu_input)
        {
            if(selPos == iconInven.transform.position)
            {
                selPos = iconSorc.transform.position;
                audioManager.NavigateMenuFx();
            }
            else if(selPos == iconSorc.transform.position)
            {
                selPos = iconInven.transform.position;
                audioManager.NavigateMenuFx();
            }
        }

        selector.transform.position = selPos;     

    }

    private void PrintMenu()
    {
        if(selPos != iconInven.transform.position && selPos != iconSorc.transform.position)
        {
            selPos = iconInven.transform.position;
        }
        else if(selPos == iconInven.transform.position)
        {
            inventory.SetActive(true);
            buttonMap.SetActive(false);
            menuName.text = "Inventaire";
            playerManager.onInventory = true; 
            //playerManager.onSorceryTree = false; //activer pour le menu Inventory
        }
        else if(selPos == iconSorc.transform.position)
        {
            buttonMap.SetActive(true);
            inventory.SetActive(false);
            menuName.text = "Commandes";
            //playerManager.onSorceryTree = true; //desactiver pour le menu Inventory
            playerManager.onInventory = false;
        }
    }
}
