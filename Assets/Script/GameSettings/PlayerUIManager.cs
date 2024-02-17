using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SJ;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] GameObject notifAchiementUI, achievementFx;
    [SerializeField] AudioManager audioManager;
    
    void Awake()
    {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Playerui"); // Recherche d'objets avec le tag spécifique

            if (objs.Length > 1) // Vérifier s'il y a déjà des objets persistants présents
            {
                // Si oui, détruire les doublons
                for (int i = 1; i < objs.Length; i++)
                {
                    Destroy(objs[i]);
                }
            }
            DontDestroyOnLoad(this.gameObject);
    }

    public void HandleCompleteQuestNotification(String stepName)
    {
        Transform notifPosition = transform.GetChild(5).GetComponent<Transform>();
        GameObject newNotif = Instantiate(notifAchiementUI, notifPosition);
        newNotif.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stepName;
    }

    public IEnumerator HandleAchievement(string stepName)
    {
        Instantiate(achievementFx, audioManager.transform.position + new Vector3 (0, 1.5f, 0f), Quaternion.identity);
        audioManager.PowerUp();
        HandleCompleteQuestNotification(stepName);
        yield return new WaitForSeconds(0.1f);
        
    }  
}
