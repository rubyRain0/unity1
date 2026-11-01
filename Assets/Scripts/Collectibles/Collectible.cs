using System;
using System.Collections;
using UnityEngine;

namespace Game.Collectibles
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Collectible : MonoBehaviour
    {
        [Header("Respawn")]
        [Tooltip("0 = респавн выключен (объект уничтожается)")]
        [SerializeField] private float respawnDelay = 2f;

        [Tooltip("Зона, внутри которой объект может появиться заново")]
        [SerializeField] private Transform spawnArea;

        [Header("Player")]
        [SerializeField] private string playerTag = "Player";

        [Tooltip("Отступ от краёв поля при случайном респавне")]
        [SerializeField] private float edgeMargin = 1f;

        private bool _triggered;
        private Renderer[] _renderers;
        private Collider2D[] _colliders;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>(true);
            _colliders = GetComponentsInChildren<Collider2D>(true);
        }

        private void Reset()
        {
            var col = GetComponent<Collider2D>();
            if (col != null) col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered) return;
            if (!other.CompareTag(playerTag)) return;

            _triggered = true;
            ApplyEffect(other.gameObject);

            if (respawnDelay > 0f)
                StartCoroutine(RespawnRoutine());
            else
                Destroy(gameObject);
        }

        protected abstract void ApplyEffect(GameObject player);

        private IEnumerator RespawnRoutine()
        {
            SetActiveState(false);
            yield return new WaitForSeconds(respawnDelay);

            TeleportToRandomPosition();
            SetActiveState(true);
            _triggered = false;
        }

        private void TeleportToRandomPosition()
        {
            if (spawnArea == null)
            {
                Debug.LogWarning($"[Collectible] {name}: spawnArea не задан, объект останется на месте.");
                return;
            }

            var fieldRenderer = spawnArea.GetComponent<Renderer>();
            if (fieldRenderer == null)
            {
                Debug.LogWarning($"[Collectible] {name}: у spawnArea нет Renderer.");
                return;
            }

            Bounds b = fieldRenderer.bounds;
            float x = UnityEngine.Random.Range(b.min.x + edgeMargin, b.max.x - edgeMargin);
            float y = UnityEngine.Random.Range(b.min.y + edgeMargin, b.max.y - edgeMargin);
            transform.position = new Vector3(x, y, transform.position.z);
        }

        private void SetActiveState(bool active)
        {
            foreach (var r in _renderers) r.enabled = active;
            foreach (var c in _colliders) c.enabled = active;
        }
    }
}
