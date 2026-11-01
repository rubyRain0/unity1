using UnityEngine;
using Game.Input;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private IInputSource _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = new KeyboardInputSource();
        }

        private void FixedUpdate()
        {
            Vector2 dir = _input.ReadDirection();
            _rb.linearVelocity = dir * moveSpeed;
        }
    }
}
