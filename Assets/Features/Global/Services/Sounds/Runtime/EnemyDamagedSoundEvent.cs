using GamePlay.Common.Damages;
using UnityEngine;

namespace Global.Services.Sounds.Runtime
{
    public readonly struct EnemyDamagedSoundEvent
    {
        public EnemyDamagedSoundEvent(GameObject target, ProjectileType type)
        {
            Target = target;
            Type = type;
        }
        
        public readonly GameObject Target;
        public readonly ProjectileType Type;
    }
}