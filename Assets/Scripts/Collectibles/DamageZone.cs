using UnityEngine;
using Game.Player;

namespace Game.Collectibles
{
    public class DamageZone : Collectible
    {
        [SerializeField] private int damage = 1;

        protected override void ApplyEffect(GameObject player)
        {
            var health = player.GetComponent<PlayerHealth>();
            if (health == null) return;

            health.TakeDamage(damage);
            Debug.Log($"[DamageZone] Player hit for {damage}");
        }
    }
}
