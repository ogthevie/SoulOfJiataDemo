using System.Collections;
using UnityEngine;
using SJ;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    public InputManager inputManager;
    public bool vasetuto, steleTuto, saveTuto, dialogTuto, paralyzeTuto, arcLightTuto, thunderTuto;
    public GameObject tipsUI, skillTuto;
    [SerializeField] TextMeshProUGUI titleText, subTitleText, descriptionText;
    [SerializeField] Image skillImage;
    [SerializeField] List <string> titleTutos = new List<string>();
    [SerializeField] List <string> subtitleTutos = new List<string>();
    [SerializeField] List <string> descriptionTutos = new List<string>();
    [SerializeField] List <Sprite> imageSkillTutos = new List<Sprite>();

    private void Start() 
    {
        inputManager = FindFirstObjectByType<InputManager>();
    }

    //Rajoutez des explications sur les numeros
    public IEnumerator HandleToggleTipsUI(string tipText)
    {
        yield return new WaitForSeconds(0.3f);
        tipsUI.SetActive(true);
        tipsUI.GetComponent<TextMeshProUGUI>().text = tipText;
        yield return new WaitForSeconds(4f);
        tipsUI.SetActive(false);
    }

    public IEnumerator HandleDescriptionSkillTutos(int id, int subId)
    {
        yield return new WaitForSeconds(4f);
        skillTuto.SetActive(true);
        titleText.text = titleTutos[id];
        subTitleText.text = subtitleTutos[subId];
        descriptionText.text = descriptionTutos[subId];
        skillImage.sprite = imageSkillTutos[subId];
        yield return new WaitForSeconds(10f);
        skillTuto.SetActive(false);
    }


}
