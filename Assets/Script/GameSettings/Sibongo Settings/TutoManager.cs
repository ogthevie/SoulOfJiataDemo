using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TutoManager : MonoBehaviour
{
    public int childIndex;

    private void Start() 
    {
        HiddenUI();    
    }

    public void HiddenUI()
    {
        transform.GetChild(childIndex).gameObject.SetActive(false);
        this.GetComponent<RectTransform>().DOAnchorPosX(400, 0.4f, false);  
    }

    public void ShowUI()
    {
        this.GetComponent<RectTransform>().DOAnchorPosX(-350, 0.2f, false);
        transform.GetChild(childIndex).gameObject.SetActive(true);
    }


}
