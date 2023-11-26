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


    void LateUpdate()
    {
        if(!playerManager.onSorceryTree)
            return;
        HandleNavigationSorceryTree();
        HandleSorceryDescription();
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
            if(!playerManager.canPur) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[0].sorcerySprite;
            sorceryName.text = sorceryDatas[0].sorceryName;
            sorceryDescription.text = sorceryDatas[0].sorceryDescription;
        }
        else if(selPos == s2.transform.position)
        {
            if(!playerManager.canSomm) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[1].sorcerySprite;
            sorceryName.text = sorceryDatas[1].sorceryName;
            sorceryDescription.text = sorceryDatas[1].sorceryDescription;            
        }
        else if(selPos == s3.transform.position)
        {
            if(!playerManager.canDest) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[2].sorcerySprite;
            sorceryName.text = sorceryDatas[2].sorceryName;
            sorceryDescription.text = sorceryDatas[2].sorceryDescription;            
        }
        else if(selPos == s4.transform.position)
        {
            if(!playerManager.canBaemb) sorcerySprite.color = blurredColor;
            else sorcerySprite.color = Color.white;

            sorcerySprite.sprite = sorceryDatas[3].sorcerySprite;
            sorceryName.text = sorceryDatas[3].sorceryName;
            sorceryDescription.text = sorceryDatas[3].sorceryDescription;            
        }
    }

}