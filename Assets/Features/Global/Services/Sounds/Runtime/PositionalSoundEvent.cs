using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    public struct PositionalSoundEvent
    {
        public PositionalSoundEvent(PositionalSoundType type, Vector2 position, GameObject target = null)
        {
            Type = type;
            Position = position;
            Target = target;
        }

        public readonly PositionalSoundType Type;
        public readonly Vector2 Position;
        public readonly GameObject Target;
    }
}