using UnityEngine;

namespace Game.Collectibles
{
    public class ScorePickup : Collectible
    {
        [SerializeField] private int scoreAmount = 10;

        protected override void ApplyEffect(GameObject player)
        {
            if (ScoreTracker.Instance != null)
                ScoreTracker.Instance.AddScore(scoreAmount);
        }
    }
}
