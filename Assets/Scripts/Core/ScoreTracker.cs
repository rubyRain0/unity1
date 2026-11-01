using System;
using UnityEngine;

namespace Game
{
    public class ScoreTracker : MonoBehaviour
    {
        public static ScoreTracker Instance { get; private set; }

        public int Score { get; private set; }

        public event Action<int> OnScoreChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Score = 0;
        }

        public void AddScore(int amount)
        {
            if (amount <= 0) return;

            Score += amount;
            Debug.Log($"[ScoreTracker] +{amount} => {Score}");
            OnScoreChanged?.Invoke(Score);
        }
    }
}
