using UnityEngine;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public readonly struct HealthChangeEvent
    {
        public HealthChangeEvent(int current, int max, GameObject target)
        {
            Target = target;
            Current = current;
            Max = max;
        }

        public readonly int Current;
        public readonly int Max;
        public readonly GameObject Target;
    }
}