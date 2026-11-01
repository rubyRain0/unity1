using UnityEngine;
using Game.Player;

namespace Game.Collectibles
{
    public class HealthPickup : Collectible
    {
        [SerializeField] private int healAmount = 1;

        protected override void ApplyEffect(GameObject player)
        {
            var health = player.GetComponent<PlayerHealth>();
            if (health == null) return;

            health.Heal(healAmount);
            Debug.Log($"[HealthPickup] +{healAmount} HP");
        }
    }
}
