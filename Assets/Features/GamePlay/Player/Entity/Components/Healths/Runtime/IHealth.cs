﻿namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public interface IHealth
    {
        int Max { get; }
        int Amount { get; }
        bool IsAlive { get; }

        void SetMaxHealth(int maxHealth, int regenerationInTick);
        void Respawn();
        void Heal(int add);
        void ApplyDamage(int damage);
    }
}