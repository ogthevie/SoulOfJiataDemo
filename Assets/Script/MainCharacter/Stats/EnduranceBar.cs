using UnityEngine;
using UnityEngine.UI;

namespace SJ
{
    public class EnduranceBar : MonoBehaviour
    {
       public Slider slider;

       void Awake()
       {
          slider = GetComponent<Slider>();   
       }

       public void SetMaxEndurance(float maxEndurance)
       {
            slider.maxValue = maxEndurance;
            slider.value = maxEndurance;
       }

       public void SetCurrentEndurance(float currentEndurance)
       {
            slider.value = currentEndurance;
       }
    
    }
}

