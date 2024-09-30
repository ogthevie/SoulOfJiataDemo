using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;
using SJ;

public class NotificationManager : MonoBehaviour
{
    PlayerManager playerManager;
    public Image tutoImage;
    public TextMeshProUGUI tutoDescription;
    public List<ConsumableData> consumableDatas = new ();

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void StartTuto(int i)
    {
        if(playerManager.onTutoScreen && i > 0)
        {
            StartCoroutine(startTuto(i));
        }

        IEnumerator startTuto(int i)
        {
            this.GetComponent<RectTransform>().DOAnchorPosX(-300f, 0.25f, false);
            tutoImage.sprite = consumableDatas[i].consumableIcon;
            tutoDescription.text = consumableDatas[i].consumableName;
            yield return new WaitForSeconds (4f);
            EndTuto();
        }
    }

    public void EndTuto()
    {
        StartCoroutine(endTuto());


        IEnumerator endTuto()
        {
            this.GetComponent<RectTransform>().DOAnchorPosX(300f, 0.25f, false);
            yield return new WaitForSeconds (2f);
            playerManager.onTutoScreen = false;
        }        
    }
}
