using System.Collections;
using UnityEngine;
using TMPro;
using Game.Player;

namespace Game.UI
{
    public class HUD : MonoBehaviour
    {
        [Header("Ссылки")]
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text scoreText;

        private PlayerHealth _playerHealth;

        private IEnumerator Start()
        {
            yield return null;

            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                _playerHealth = player.GetComponent<PlayerHealth>();

            if (_playerHealth != null)
            {
                _playerHealth.OnHealthChanged += UpdateHealth;
                UpdateHealth(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
            }

            if (Game.ScoreTracker.Instance != null)
            {
                Game.ScoreTracker.Instance.OnScoreChanged += UpdateScore;
                UpdateScore(Game.ScoreTracker.Instance.Score);
            }
        }

        private void OnDestroy()
        {
            if (_playerHealth != null)
                _playerHealth.OnHealthChanged -= UpdateHealth;

            if (Game.ScoreTracker.Instance != null)
                Game.ScoreTracker.Instance.OnScoreChanged -= UpdateScore;
        }

        private void UpdateHealth(int current, int max)
        {
            if (healthText != null)
                healthText.text = $"Здоровье: {current}/{max}";
        }

        private void UpdateScore(int score)
        {
            if (scoreText != null)
                scoreText.text = $"Очки: {score}";
        }
    }
}
