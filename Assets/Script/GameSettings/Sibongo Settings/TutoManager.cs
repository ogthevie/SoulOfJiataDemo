using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TutoManager : MonoBehaviour
{
    public bool vasetuto, steleTuto, saveTuto, dialogTuto, paralyzeTuto, arcLightTuto, thunderTuto;
    public GameObject tipsUI, trainingUI;
    private void Start() 
    {
        trainingUI = transform.GetChild(0).gameObject;
        tipsUI = transform.GetChild(1).gameObject;
        HiddenUI();    
    }

    public void HiddenUI()
    {
        this.GetComponent<RectTransform>().DOAnchorPosX(400, 0.4f, false);  
    }

    public void ShowUI()
    {
        this.GetComponent<RectTransform>().DOAnchorPosX(-300, 0.2f, false);
    }

    public IEnumerator HandleToggleTipsUI(string tipText)
    {
        tipsUI.SetActive(true);
        trainingUI.SetActive(false);
        tipsUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = tipText;
        ShowUI();
        yield return new WaitForSeconds(7f);
        HiddenUI();
        yield return new WaitForSeconds(2f);
        tipsUI.SetActive(false);
    }


}
