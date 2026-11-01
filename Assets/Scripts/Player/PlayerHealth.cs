using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;

        public int MaxHealth => maxHealth;
        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public event Action<int, int> OnHealthChanged;
        public event Action OnDied;

        private bool _restarting;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || IsDead || _restarting) return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            Debug.Log($"[PlayerHealth] Damage -{amount}, HP: {CurrentHealth}/{maxHealth}");
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

            if (CurrentHealth <= 0)
                Die();
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || IsDead) return;

            CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
            Debug.Log($"[PlayerHealth] Heal +{amount}, HP: {CurrentHealth}/{maxHealth}");
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        }

        private void Die()
        {
            _restarting = true;
            Debug.Log("[PlayerHealth] HP reached 0 - restarting level.");
            OnDied?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
