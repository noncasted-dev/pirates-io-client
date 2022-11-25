using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    public struct PositionalSoundEvent
    {
        public PositionalSoundEvent(PositionalSoundType type, Vector2 position)
        {
            Type = type;
            Position = position;
        }
        
        public readonly PositionalSoundType Type;
        public readonly Vector2 Position;
    }
}