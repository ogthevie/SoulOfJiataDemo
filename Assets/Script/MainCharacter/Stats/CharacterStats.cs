using UnityEngine;

namespace SJ
{
    public abstract class CharacterStats : MonoBehaviour
    {
        public HealthBar healthBar;
        public int healthLevel = 1;
        public int staminaLevel = 10;
        public float enduranceLevel = 1; 
        public int maxHealth;
        public int currentHealth;
        public int maxStamina;
        public int currentStamina;
        public float currentEndurance;
        public float maxEndurance;
    }
}

