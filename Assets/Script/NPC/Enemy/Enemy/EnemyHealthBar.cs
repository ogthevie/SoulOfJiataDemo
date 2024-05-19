using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fillColor;

    protected virtual void Awake()
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
        slider.DOValue(currentHealth, 0.4f, false);
    }
}
