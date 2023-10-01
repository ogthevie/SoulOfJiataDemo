using UnityEngine;

namespace SJ
{
    public class DamagePlayer : MonoBehaviour
    {
        readonly int damage = 40;
        private void OnTriggerEnter(Collider other)
        {
             PlayerStats playerStats = other.GetComponent<PlayerStats>();

             if (playerStats != null)
             {
                playerStats.TakeDamage(damage);
             }

        }
    }

}
