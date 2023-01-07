using System;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public interface ISail
    {
        int Strength { get; }

        event Action Changed;

        void Damage(float damage);
    }
}