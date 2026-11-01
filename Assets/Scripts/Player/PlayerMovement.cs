using UnityEngine;
using Game.Input;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private VirtualJoystick joystick;
        
        [Header("Tilt Settings")]
        [SerializeField] private bool useTiltInput;
        [Range(0.1f, 10f)] [SerializeField] private float tiltSensitivity = 2.5f;
        [Range(0f, 0.5f)] [SerializeField] private float tiltDeadZone = 0.08f;

        private Rigidbody2D _rb;
        private IInputSource _keyboard;
        private IInputSource _joystickInput;
        private IInputSource _tiltInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _keyboard = new KeyboardInputSource();
            _joystickInput = joystick != null ? new JoystickInputSource(joystick) : null;
            
            _tiltInput = useTiltInput ? new TiltInputSource(tiltSensitivity, tiltDeadZone) : null;
        }

       private void FixedUpdate()
        {
            Vector2 dir = Vector2.zero;
            string activeSource = "None";

            if (_keyboard != null)
            {
                Vector2 kDir = _keyboard.ReadDirection();
                if (kDir.sqrMagnitude > 0.01f) { dir = kDir; activeSource = "Keyboard"; }
            }

            if (dir.sqrMagnitude < 0.01f && _joystickInput != null)
            {
                Vector2 jDir = _joystickInput.ReadDirection();
                if (jDir.sqrMagnitude > 0.01f) { dir = jDir; activeSource = "Joystick"; }
            }

            if (dir.sqrMagnitude < 0.01f && _tiltInput != null)
            {
                Vector2 tDir = _tiltInput.ReadDirection();
                if (tDir.sqrMagnitude > 0.01f) { dir = tDir; activeSource = "Tilt"; }
            }

            // Отладка
            if (Time.frameCount % 30 == 0) 
            {
                Debug.Log($"Source: {activeSource} | Dir: ({dir.x:F2}, {dir.y:F2})");
            }

            _rb.linearVelocity = dir * moveSpeed;
        }
    }
}