using System.Collections;
using UnityEngine;
using DG.Tweening;
using SJ;
using TMPro;

public class NotificationQuestManager : MonoBehaviour
{
    [SerializeField] GameObject achievementFx;
    [SerializeField] AudioManager audioManager;
    [SerializeField] TextMeshProUGUI questTitle;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public IEnumerator StartHandleAchievement(string stepName)
    {
        //Instantiate(achievementFx, audioManager.transform.position + new Vector3 (0, 1.5f, 0f), Quaternion.identity);
        questTitle.text = stepName;
        yield return new WaitForSeconds(0.1f);
        audioManager.PowerUp();
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<RectTransform>().DOAnchorPosX(-200f, 0.25f, false);
        yield return new WaitForSeconds(5f);  
        this.GetComponent<RectTransform>().DOAnchorPosX(205f, 0.25f, false);
    } 
}
