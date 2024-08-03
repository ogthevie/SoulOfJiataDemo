using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class StoryManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] List <String> storyTitles = new ();
    [TextArea(1,4)] [SerializeField] List <String> storyBriefings = new ();
    [SerializeField] TextMeshProUGUI storyTitle, storyBrief;

    public int storyStep = -1; //jeu d'equilibre avec le curseur de quête


    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    /*
    le numero definit la mission dans laquelle nous sommes actu
        0 = discussion avec Isamal
        1 = discussion avec Bilol
        2 = discussion avec Big Kossi
        3 = le second brassard + discussion avec Bomboktan
        31 = arcLight + discussion avec Bomboktan
        32 = sommeil du cadavre + discussion avec Bomboktan
        4 = nson + discussion avec Bomboktan
        5 = apparition Kossi
        6 = esprit de la pierre
    */

    public void checkstoryStep(bool canNextStep)
    {
        if(canNextStep) 
        {
            storyStep += 1;
            gameManager.canNotif = true;
            if(storyStep == 2) FindObjectOfType<SibongoManager>().KossiPortal.SetActive(true);
            GetComponent<GameSaveManager>().SaveAllData();
        }
        
        UpdateSynopsisPauseMenu();
        return;
    }

    public void UpdateSynopsisPauseMenu()
    {
        if(storyStep < 0) return;

        if(storyStep == 0 || storyStep == 1 || storyStep == 2 || storyStep == 3 || storyStep == 4 || storyStep == 6 || storyStep == 7)
        {
            storyTitle.text = storyTitles[storyStep];
            storyBrief.text = storyBriefings[storyStep];
        }
        else
        {
            storyTitle.text = storyTitles[3];
            storyBrief.text = storyBriefings[3];            
        }
    }

}
