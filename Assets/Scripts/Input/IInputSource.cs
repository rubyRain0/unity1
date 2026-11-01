using UnityEngine;

namespace Game.Input
{
    public interface IInputSource
    {
        Vector2 ReadDirection();
    }
}
