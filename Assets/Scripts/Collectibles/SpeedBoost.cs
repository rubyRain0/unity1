using UnityEngine;
using Game.Player;

namespace Game.Collectibles
{
    public class SpeedBoost : Collectible
    {
        [SerializeField] private float multiplier = 1.8f;
        [SerializeField] private float duration = 3f;

        protected override void ApplyEffect(GameObject player)
        {
            var movement = player.GetComponent<PlayerMovement>();
            if (movement == null) return;

            movement.ApplySpeedBoost(multiplier, duration);
            Debug.Log($"[SpeedBoost] x{multiplier} for {duration}s");
        }
    }
}
