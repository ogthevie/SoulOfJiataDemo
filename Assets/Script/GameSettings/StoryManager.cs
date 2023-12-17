using UnityEngine;

public class StoryManager : MonoBehaviour
{
    /*
    le numero definit la mission dans laquelle nous sommes actu
        0 = NDAP KOKOA -------- il parle avec Isamal sur le village
        1 = HISTOIRE...RACONTE -------- il voit Bilol sous la recommandation de Isamal + cinematique
        2 = HERITAGE -------- il a prend le brassard droit et doit passer le rite de NgogLituba pour
                                        heriter du savoir de ses êtres. Mais avant ça, il serait prudent de voir Libum pour s'entrainer
                                        une dernière fois car on ne sait pas ce qu'il va trouver là bas
        3 = LA VOIE DU HEROS ------- il s'entraine avec Libum
        4 = SURCHARGE ------ il prend le 2nd brassard
        5 = SOMMEIL ------- il prend le sommeil
        6 = BIG KOSSI ------ il bat BigKossi
        7 = DEPART POUR DANAY ------ il sort de la grotte
    */
    public int storyStep;
    void Awake()
    {
        storyStep = 0;
    }

}
