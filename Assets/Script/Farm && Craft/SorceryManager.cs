using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SJ;
using TMPro;

public class SorceryManager : MonoBehaviour
{
    InputManager inputManager;
    AudioManager audioManager;
    PlayerManager playerManager;
    public InventoryData inventory;
    SorceryPadManager sorceryPadManager;
    public StatesCharacterData statesJiataData;
    public List<SorceryData> sorceryDatas = new ();
    public Image sorcerySprite;
    public TextMeshProUGUI sorceryDescription;
    public TextMeshProUGUI sorceryName;
    //public List<SorceryData> slotUpSorcery = new List<SorceryData>();
    //public List<SorceryData> slotDownSorcery = new List<SorceryData>();
    //public List<SorceryData> slotLeftSorcery = new List<SorceryData>();
    //public List<SorceryData> slotRightSorcery = new List<SorceryData>();

    GameObject s1, s2, s3, s4, selector;
    Vector3 selPos;
    readonly string hexColor = "#1D1D1D";
    

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        audioManager = FindObjectOfType<AudioManager>();
        inputManager = FindObjectOfType<InputManager>();
        sorceryPadManager = FindObjectOfType<SorceryPadManager>();
    }


    void Start()
    {
        s1 = GameObject.Find("S1");
        s2 = GameObject.Find("S2");
        s3 =  GameObject.Find("S3");
        s4 = GameObject.Find("S4");
        selector = GameObject.Find("Selector");
        selPos = selector.transform.position;
    }


    void Update()
    {
        if(!playerManager.onSorceryTree)
            return;
        HandleNavigationSorceryTree();
        HandleSorceryDescription();
        HandleActivationSorcery();
    }

    void HandleNavigationSorceryTree()
    {
        if(inputManager.up_input)
        {
            if(selPos == s2.transform.position || selPos == s3.transform.position || selPos == s4.transform.position)
            {
                selPos = s1.transform.position;
                audioManager.ReadNavigationInventoryFx();
            }
            else
                audioManager.ImpossibleChoiceFx();

            //Autres deplacements
        }

        else if(inputManager.down_input)
        {
            if(selPos == s1.transform.position || selPos == s2.transform.position || selPos == s4.transform.position)
            {
                selPos = s3.transform.position;
                audioManager.ReadNavigationInventoryFx();
            }
            else
                audioManager.ImpossibleChoiceFx();

        }

        else if(inputManager.left_input)
        {
            if(selPos == s1.transform.position || selPos == s2.transform.position || selPos == s3.transform.position)
            {
                selPos = s4.transform.position;
                audioManager.ReadNavigationInventoryFx();
            }
            else
                audioManager.ImpossibleChoiceFx();

        }

        else if(inputManager.right_input)
        {
            if(selPos == s1.transform.position || selPos == s4.transform.position || selPos == s3.transform.position)
            {
                selPos = s2.transform.position;
                audioManager.ReadNavigationInventoryFx();
            }
            else
                audioManager.ImpossibleChoiceFx();
        }

        if(selPos != s1.transform.position && selPos != s4.transform.position && selPos != s3.transform.position && selPos != s2.transform.position)
        {
            selPos = s1.transform.position;
        }

        selector.transform.position = selPos;
    }

    void HandleSorceryDescription()
    {
        ColorUtility.TryParseHtmlString(hexColor, out Color blurredColor);

        if(selPos == s1.transform.position)
        {
            if(!inventory.canPur) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[0].sorcerySprite;
            sorceryName.text = sorceryDatas[0].sorceryName;
            sorceryDescription.text = sorceryDatas[0].sorceryDescription;
        }
        else if(selPos == s2.transform.position)
        {
            if(!inventory.canSomm) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[1].sorcerySprite;
            sorceryName.text = sorceryDatas[1].sorceryName;
            sorceryDescription.text = sorceryDatas[1].sorceryDescription;            
        }
        else if(selPos == s3.transform.position)
        {
            if(!inventory.canDest) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[2].sorcerySprite;
            sorceryName.text = sorceryDatas[2].sorceryName;
            sorceryDescription.text = sorceryDatas[2].sorceryDescription;            
        }
        else if(selPos == s4.transform.position)
        {
            if(!inventory.canBaemb) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[3].sorcerySprite;
            sorceryName.text = sorceryDatas[3].sorceryName;
            sorceryDescription.text = sorceryDatas[3].sorceryDescription;            
        }
    }

    void HandleActivationSorcery()
    {
        if(inventory.haveJujube && inventory.haveKatorro) inventory.canPur = true; else    inventory.canPur = false;
        if(inventory.haveEauCad && inventory.haveCorde) inventory.canSomm = true; else inventory.canSomm = false;
        if(inventory.haveClouMoluk && inventory.havePierreSel) inventory.canDest = true; else inventory.canDest = false;
        if(inventory.haveDentBK && inventory.havePierreBaemb) inventory.canBaemb = true; else inventory.canBaemb = false;

        if(!inventory.canPur) s1.GetComponent<Image>().enabled = false; 
        else 
        {
            s1.GetComponent<Image>().enabled = true;
            statesJiataData.sNorth = s1.GetComponent<Image>().sprite;
            sorceryPadManager.sorcerPalettes[0].sprite = statesJiataData.sNorth;            
        }

        if(!inventory.canSomm) s2.GetComponent<Image>().enabled = false; 
        else
        {
            s2.GetComponent<Image>().enabled = true;
            statesJiataData.sEast = s2.GetComponent<Image>().sprite;
            sorceryPadManager.sorcerPalettes[1].sprite = statesJiataData.sEast;           
        }

        if(!inventory.canDest) s3.GetComponent<Image>().enabled = false;
        else
        {
            s3.GetComponent<Image>().enabled = true;
            statesJiataData.sSouth = s3.GetComponent<Image>().sprite;
            sorceryPadManager.sorcerPalettes[2].sprite = statesJiataData.sSouth;  
        }

        if(!inventory.canBaemb) s4.GetComponent<Image>().enabled = false;else
        {
            s4.GetComponent<Image>().enabled = true;
            statesJiataData.sWest = s4.GetComponent<Image>().sprite;
            sorceryPadManager.sorcerPalettes[3].sprite = statesJiataData.sWest;  
        }
    }

}