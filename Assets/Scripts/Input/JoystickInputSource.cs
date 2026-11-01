using UnityEngine;

namespace Game.Input
{
    public class JoystickInputSource : IInputSource
    {
        private readonly VirtualJoystick _joystick;

        public JoystickInputSource(VirtualJoystick joystick)
        {
            _joystick = joystick;
        }

        public Vector2 ReadDirection()
        {
            return _joystick != null ? _joystick.Value : Vector2.zero;
        }
    }
}
