using UnityEngine;
using TMPro;
using SJ;
using System.Collections.Generic;
using DG.Tweening;

public class PauseMenuManager : MonoBehaviour
{
    InputManager inputManager;
    StoryManager storyManager;
    AudioManager audioManager;
    public GameObject selector;
    [SerializeField] List <GameObject> iconNavs = new ();
    [SerializeField] GameObject windowMenu;
    public TextMeshProUGUI menuName;
    Vector3 selPos;
    public int pauseMenuPos, indexIconNav;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        storyManager = FindObjectOfType<StoryManager>();
        audioManager = FindObjectOfType<AudioManager>();
        selector.transform.position = iconNavs[0].transform.position;
        selPos = selector.transform.position;
        Debug.Log(Screen.width);
        storyManager.checkstoryStep(false);
        //Time.timeScale = 0f;
    }

    void OnEnable()
    {
        
        transform.parent.GetComponent<PlayerUIManager>().HiddenUI();
    }

    void OnDisable()
    {
        transform.parent.GetComponent<PlayerUIManager>().ShowUI();

    }

    void Update()
    {
        HandleNavigationMenu();
        PrintMenu();
        Debug.Log(iconNavs.Count);      
    }



    private void HandleNavigationMenu() //Pensez à faire  une liste ici
    {
        if(inputManager.left_menu_input)
        {
            if(indexIconNav == 0)
            {
                audioManager.ImpossibleChoiceFx();
            }
            else
            {
                indexIconNav -= 1;
                selPos = iconNavs[indexIconNav].transform.position;
                pauseMenuPos = indexIconNav;
                audioManager.NavigateMenuFx();
            }
        }
        else if(inputManager.right_menu_input)
        {
            if(indexIconNav >= (iconNavs.Count-1)) 
            {
                audioManager.ImpossibleChoiceFx();
            }
            else
            {
                indexIconNav += 1;
                selPos = iconNavs[indexIconNav].transform.position;
                pauseMenuPos = indexIconNav;
                audioManager.NavigateMenuFx();
            }     
        }
        selector.transform.position = selPos;
    

    }

    private void PrintMenu()
    {
        if(selPos == iconNavs[0].transform.position)
        {
            menuName.text = "Histoire";
            windowMenu.GetComponent<RectTransform>().DOAnchorPosX(0, 0.5f, false);
        }
        else if(selPos == iconNavs[1].transform.position)
        {
            menuName.text = "Commandes";
            windowMenu.GetComponent<RectTransform>().DOAnchorPosX(-1080, 0.5f, false);
        }
    }
}
