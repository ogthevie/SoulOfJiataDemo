using UnityEngine;

public class PlayerGameCaseManager : MonoBehaviour
{
    SageQuestManager sageQuestManager;
    public int indexGameCase;

    void Awake()
    {
        sageQuestManager = FindObjectOfType<SageQuestManager>();
    }

    void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.layer == 3)
        {
            if(sageQuestManager.canPlay)
            {
                sageQuestManager.cursorPosition = indexGameCase;
                if(sageQuestManager.gameCase[indexGameCase] != 0) //vérifier s'il y a des boules
                {
                    sageQuestManager.Playerplay();
                }
            }
        }
    }
}
