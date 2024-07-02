using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TutoManager : MonoBehaviour
{
    private void Start() 
    {
        HiddenUI();    
    }

    public void HiddenUI()
    {
        this.GetComponent<RectTransform>().DOAnchorPosX(400, 0.4f, false);  
    }

    public void ShowUI()
    {
        this.GetComponent<RectTransform>().DOAnchorPosX(-420, 0.2f, false);
    }


}
