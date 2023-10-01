using UnityEngine;
using UnityEngine.UI;

namespace SJ
{
        public class StaminaBar : MonoBehaviour
    {
        public Slider slider;

        void Awake()
        {
            slider = GetComponent<Slider>();    
        }

        public void SetMaxStamina(int maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }

        public void SetCurrentStamina(int currentStamina)
        {
            slider.value = currentStamina;
        }
    }

}
