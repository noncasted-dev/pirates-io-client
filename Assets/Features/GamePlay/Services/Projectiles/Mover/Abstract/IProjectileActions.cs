﻿using GamePlay.Common.Damages;

namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IProjectileActions
    {
        bool IsLocal { get; }
        string CreatorId { get; }
        ProjectileType Type { get; }
        void OnTriggered(IDamageReceiver damageReceiver);
        void Destroy();
        void OnCollided();
    }
}