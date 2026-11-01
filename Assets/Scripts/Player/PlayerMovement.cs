using UnityEngine;
using Game.Input;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private VirtualJoystick joystick;

        private Rigidbody2D _rb;
        private IInputSource _keyboard;
        private IInputSource _joystickInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _keyboard = new KeyboardInputSource();
            _joystickInput = joystick != null ? new JoystickInputSource(joystick) : null;
        }

        private void FixedUpdate()
        {
            Vector2 dir = _keyboard.ReadDirection();
            if (dir.sqrMagnitude < 0.01f && _joystickInput != null)
            {
                dir = _joystickInput.ReadDirection();
            }

            _rb.linearVelocity = dir * moveSpeed;
        }
    }
}
