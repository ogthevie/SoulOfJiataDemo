using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SageQuestManager : MonoBehaviour
{
 public int [] gameCase = new int[12];
 public bool canPlay;
 public int cursorPosition;
 public int collector;
 int bomboktanScore, JiataScore;
 
 void Start()
 {
    for(int i = 0; i < gameCase.Length; i++)
    {
        gameCase[i] = 4;
    }
    
    canPlay = true;
 }

 void IaPlay()
 {
   
    do                                              /*choisir une case au hasard*/
    {
        cursorPosition = Random.Range(0,6);
        collector = gameCase[cursorPosition];
    }while(collector == 0);                        //verifier s'il y a des boules à l'intérieur

    for(int i = 0; i < collector; i++)              //Deplacer les boules
    {
        if(cursorPosition == 12)
        {
            cursorPosition = 0;
        }
        gameCase[cursorPosition] += i;
        cursorPosition += i;
    }
    
    if(gameCase[cursorPosition] == 2) //placer le score
    {
        bomboktanScore += 1;
        if(gameCase[cursorPosition - 1] == 1 || gameCase[cursorPosition - 1] == 2)
        {
            bomboktanScore += 1;
            canPlay = true;    //tour de Jiata
        }
    }

 }

 public void Playerplay()
 {
    collector = gameCase[cursorPosition];//choisir une case

    for(int i = 0; i < collector; i++)              //Deplacer les boules
    {
        if(cursorPosition == 12)     //déplacer les boules
        {
            cursorPosition = 0;
        }
        gameCase[cursorPosition] += i;
        cursorPosition += i;
    }

    if(gameCase[cursorPosition] == 2) //placer le score
    {
        bomboktanScore += 1;
        if(gameCase[cursorPosition - 1] == 1 || gameCase[cursorPosition - 1] == 2)
        {
            JiataScore += 1;
            canPlay = false;    //tour de Jiata
        }
    } 

 }

 void EndGame()
 {
    if(JiataScore > 24 || bomboktanScore > 24)
    {
        Debug.Log("Partie terminée");
    }
    //Lancer une cinematique en fonction de lq victoire
 }



}
