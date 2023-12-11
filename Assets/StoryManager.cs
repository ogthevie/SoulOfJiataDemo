using UnityEngine;

public class StoryManager : MonoBehaviour
{
    /*
    le numero definit la mission dans laquelle nous sommes actu
        1 = NDAP KOKOA -------- il parle avec Isamal sur le village
        2 = HISTOIRE...RACONTE -------- il voit Bilol sous la recommandation de Isamal + cinematique
        3 = HERITAGE -------- il a prend le brassard droit et doit passer le rite de NgogLituba pour
                                        heriter du savoir de ses êtres. Mais avant ça, il serait prudent de voir Libum pour s'entrainer
                                        une dernière fois car on ne sait pas ce qu'il va trouver là bas
        4 = LA VOIE DU HEROS ------- il s'entraine avec Libum
        5 = SURCHARGE ------ il prend le 2nd brassard
        60 = SOMMEIL ------- il prend le sommeil
        61 = ARCLIGHT && FORCEKOSSI ----- il prend les deux et veut affronter BigKossi
        7 = BIG KOSSI ------ il bat BigKossi
        8 = DEPART POUR DANAY ------ il sort de la grotte
    */
    public int storyStep;
}
