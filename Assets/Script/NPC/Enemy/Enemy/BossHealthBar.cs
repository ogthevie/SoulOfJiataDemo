using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SJ;

public class BossHealthBar : EnemyHealthBar
{
    CameraManager cameraManager;
    BuffaloManager buffaloManager;
    public GameObject bossHUD;
    protected override void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        buffaloManager = GetComponent<BuffaloManager>();
    }

    void OnEnable()
    {
        bossHUD = FindObjectOfType<PlayerUIManager>().transform.GetChild(1).gameObject;
        slider = bossHUD.GetComponentInChildren<Slider>();
        fillColor = bossHUD.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        SetMaxHealth(buffaloManager.currentHealth);       
    }
}
