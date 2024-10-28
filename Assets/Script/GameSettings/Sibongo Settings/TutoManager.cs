using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TutoManager : MonoBehaviour
{
    public bool vasetuto, steleTuto, saveTuto, dialogTuto, paralyzeTuto, arcLightTuto, thunderTuto;
    public GameObject tipsUI;
    private void Start() 
    {
        tipsUI = transform.GetChild(0).gameObject;
    }

    public IEnumerator HandleToggleTipsUI(string tipText)
    {
        yield return new WaitForSeconds(0.5f);
        tipsUI.SetActive(true);
        tipsUI.GetComponent<TextMeshProUGUI>().text = tipText;
        yield return new WaitForSeconds(6f);
        tipsUI.SetActive(false);
    }


}
