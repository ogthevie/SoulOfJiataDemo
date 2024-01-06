using UnityEngine;

public class StoryManager : MonoBehaviour
{
    /*
    le numero definit la mission dans laquelle nous sommes actu
        0 = NDAP KOKOA -------- il parle avec Isamal sur le village
        1 = HISTOIRE...RACONTE -------- il voit Bilol sous la recommandation de Isamal
        2 = LA VOIE DU HEROS ------- il s'entraine avec Libum
        3 = HERITAGE -------- il revient chez Bilol qui va lui donner le crâne du chat, qui est un outil qui va lui permettre 
                                de stocker tout ce qu'il peut rammasser et va lui dire qu'il peut partir
        4 = Grotte Kossi et SURCHARGE ------ il prend le 2nd brassard
        5 = SOMMEIL ------- il prend le sommeil
        6 = BIG KOSSI ------ il bat BigKossi
        7 = DEPART POUR DANAY ------ il sort de la grotte
    */
    public int storyStep = 0;
    public void checkstoryStep(bool canNextStep)
    {

        if(storyStep == 0 && canNextStep) storyStep = 1;
        else if(storyStep == 1 && canNextStep) storyStep = 2;
        else if(storyStep == 2 && canNextStep) storyStep = 21; 
        else if(storyStep == 3 && canNextStep) storyStep = 4;
        return;
    }

}
