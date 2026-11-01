using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class KeyboardInputSource : IInputSource
    {
        public Vector2 ReadDirection()
        {
            var kb = Keyboard.current;
            if (kb == null)
            {
                return Vector2.zero;
            }

            Vector2 dir = Vector2.zero;

            if (kb.wKey.isPressed || kb.upArrowKey.isPressed) dir.y += 1f;
            if (kb.sKey.isPressed || kb.downArrowKey.isPressed) dir.y -= 1f;
            if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) dir.x += 1f;
            if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) dir.x -= 1f;

            return dir.sqrMagnitude > 1f ? dir.normalized : dir;
        }
    }
}
