using GamePlay.Common.Damages;
using GamePlay.Services.Projectiles.Entity;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Local.Replicators.Canons.Runtime
{
    public interface ICannonReplicator
    {
        string PlayerId { get; }

        void Replicate(
            ProjectileType type,
            Vector2 position,
            float angle,
            float speed,
            int damage,
            float distance);
    }
}