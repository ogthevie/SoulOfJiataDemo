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
        public Color maxColor;
        public List <VolumeProfile> volumeProfiles = new ();

        void Awake()
        {
            slider = GetComponent<Slider>();
            //globalVolume = FindObjectOfType<Volume>();
            globalVolume = FindObjectOfType<GlobalVolumeManager>().GetComponent<Volume>();
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
            
        }
    }

}
