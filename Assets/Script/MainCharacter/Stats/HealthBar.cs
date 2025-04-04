using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using DG.Tweening;

namespace SJ
{
        public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Volume globalVolume;
        public Color maxHP, midHP, lowHP;
        [SerializeField] Image fillColor;
        public List <VolumeProfile> volumeProfiles = new ();

        void Awake()
        {
            slider = GetComponent<Slider>();
            //globalVolume = FindFirstObjectByType<Volume>();
            globalVolume = FindFirstObjectByType<GlobalVolumeManager>().GetComponent<Volume>();
        }

        private void Start()
        {
            globalVolume.profile = volumeProfiles[0];
            globalVolume.weight = 0.55f;
        }
        
        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
            HandleSlider();
        }

        public void HandleSlider()
        {
            if(slider.value < (slider.maxValue * 0.7f))
            {
                globalVolume.profile = volumeProfiles[1];
                globalVolume.weight = (float)(-0.0128 * (float)slider.value + 1f);
            }
            else if(slider.value >= (slider.maxValue * 0.7f) && globalVolume.profile == volumeProfiles[1])
            {
                globalVolume.profile = volumeProfiles[0];
                globalVolume.weight = 0.55f;
            }

            HandleColorSlider();
            
        }

        void HandleColorSlider()
        {
            if(slider.value <= (slider.maxValue * 0.7f) && slider.value > (slider.maxValue * 0.4f)) fillColor.color = midHP;
            else if(slider.value <= (slider.maxValue * 0.4f)) fillColor.color = lowHP;
            else fillColor.color = maxHP;
        }
    }

}
