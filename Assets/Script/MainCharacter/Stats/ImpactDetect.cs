using UnityEngine;

namespace SJ
{
    public class ImpactDetect : MonoBehaviour
    {
        [Header("Stamina Costs")]
        PlayerStats playerStats;     
        public int baseStamina = 10;
        private float highAttackCoeff = 4f;
        public float chargeCoeff = 1.5f;
        public int currentAttackDamage = 25;

        

        void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
        }

        public void DrainStaminaHighAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(baseStamina * highAttackCoeff));
        }
    }
}


