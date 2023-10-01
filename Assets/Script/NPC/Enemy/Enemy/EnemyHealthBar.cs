using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fillColor;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
