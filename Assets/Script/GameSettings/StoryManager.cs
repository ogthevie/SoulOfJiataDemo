using UnityEngine;

public class StoryManager : MonoBehaviour
{
    GameSaveManager gameSaveManager;

    void Start()
    {
        gameSaveManager = GetComponent<GameSaveManager>();
    }
    /*
    le numero definit la mission dans laquelle nous sommes actu
        0 = NDAP KOKOA -------- il parle avec Isamal sur le village
        1 = HISTOIRE...RACONTE -------- il voit Bilol sous la recommandation de Isamal/Libum. Bilol lui explique la grotte kossi mais
                                        lui dit qu'il doit chercher l'homme dans la pierre et entendre ce qu'il a à lui dire
        2 = LA VOIE DU HEROS ------- il part voir l'homme dans la pierre qui lui dit qu'il doit le juger avant d'accepter ou non son
                                        entrée dans la grotte, il lui donne le 1er brassard.
        3 = HERITAGE -------- il combat et revoit l'homme dans la pierre qui accepte sa requete et lui dit se rendre maintenant au tunnel
                                de la colline
        4 = Grotte Kossi et SURCHARGE ------ il prend le 2nd brassard
        5 = SOMMEIL ------- il prend le sommeil
        6 = BIG KOSSI ------ il bat BigKossi
        7 = DEPART POUR DANAY ------ il sort de la grotte
    */
    public int storyStep = -1; //jeu d'equilibre avec le curseur de quête

    public void checkstoryStep(bool canNextStep)
    {
        if(storyStep == -1 && canNextStep) 
        {
            storyStep = 0;
            gameSaveManager.SaveAllData();
        }
        else if(storyStep == 0 && canNextStep)
        {
            storyStep = 1;
            gameSaveManager.SaveAllData();
        }
        else if(storyStep == 1 && canNextStep)
        {
            storyStep = 2;
            gameSaveManager.SaveAllData();
        }
        else if(storyStep == 2 && canNextStep)
        {
            storyStep = 3;
            gameSaveManager.SaveAllData();
        } 
        else if(storyStep == 3 && canNextStep)
        {
            storyStep = 4;
            gameSaveManager.SaveAllData();
        }
        
        return;
    }

}
