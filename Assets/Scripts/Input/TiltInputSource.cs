using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class TiltInputSource : IInputSource
    {
        private float _deadZone;
        private float _sensitivity;
        
        private bool _calibrated;
        private Vector2 _zero;

        public TiltInputSource(float sensitivity = 2.5f, float deadZone = 0.08f)
        {
            _sensitivity = sensitivity;
            _deadZone = deadZone;
        }

        public Vector2 ReadDirection()
        {
            Vector2 raw = ReadRawTilt();

            if (!_calibrated)
            {
                _zero = raw;
                _calibrated = true;
            }

            Vector2 tilt = raw - _zero;
            tilt = new Vector2(ApplyDeadZone(tilt.x), ApplyDeadZone(tilt.y));

            return Vector2.ClampMagnitude(tilt * _sensitivity, 1f);
        }

        private float ApplyDeadZone(float v)
        {
            if (Mathf.Abs(v) < _deadZone) return 0f;
            return Mathf.Sign(v) * (Mathf.Abs(v) - _deadZone) / (1f - _deadZone);
        }

        private Vector2 ReadRawTilt()
        {
            // тест на пк (IJKL)
            if (Keyboard.current != null)
            {
                float x = 0f, y = 0f;
                if (Keyboard.current.jKey.isPressed) x = -1f;
                if (Keyboard.current.lKey.isPressed) x = 1f;
                if (Keyboard.current.kKey.isPressed) y = -1f;
                if (Keyboard.current.iKey.isPressed) y = 1f;

                if (x != 0f || y != 0f)
                    return new Vector2(x, y);
            }

            // для мобилок
            var accel = Accelerometer.current;
            if (accel != null && accel.enabled)
            {
                Vector2 a = accel.acceleration.ReadValue();
                return new Vector2(a.x, a.y);
            }

            return Vector2.zero;
        }
    }
}