using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace SJ
{
        public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Volume globalVolume;
        public Image fillColor;
        public Color criticalColor;
        public Color maxColor;
        public List <VolumeProfile> volumeProfiles = new ();

        void Awake()
        {
            slider = GetComponent<Slider>();
            globalVolume = FindObjectOfType<Volume>();
            globalVolume = FindObjectOfType<GlobalVolumeManager>().GetComponent<Volume>();
        }

        private void Start()
        {
            globalVolume.profile = volumeProfiles[0];
            globalVolume.weight = 0.5f;
            fillColor.color = maxColor;
        }

        void Update()
        {
            HandleSlider();
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

        public void HandleSlider()
        {
            if(slider.value < (slider.maxValue * 0.5f))
            {
                if(globalVolume.profile != volumeProfiles[1])
                {
                    globalVolume.profile = volumeProfiles[1];
                    globalVolume.weight = 0.6f;
                    fillColor.color = criticalColor;
                }
                else
                {
                    if(slider.value > (slider.maxValue * 0.3))
                        globalVolume.weight = 0.8f;
                    else if(slider.value >= (slider.maxValue * 0.2) && slider.value <= (slider.maxValue * 0.3))
                        globalVolume.weight = 0.9f;
                    else
                        globalVolume.weight = 1f;
                }

            }
            else if(slider.value >= (slider.maxValue * 0.5f) && globalVolume.profile == volumeProfiles[1])
            {
                globalVolume.profile = volumeProfiles[0];
                globalVolume.weight = 0.5f;
                fillColor.color = maxColor;
            }
            
        }
    }

}
