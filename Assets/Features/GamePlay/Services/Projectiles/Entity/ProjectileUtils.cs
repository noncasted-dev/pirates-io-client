using System;
using GamePlay.Common.Damages;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.Projectiles.Entity
{
    public static class ProjectileUtils
    {
        public static ItemType ConvertToItemType(this ProjectileType type)
        {
            return type switch
            {
                ProjectileType.Ball => ItemType.CannonBall,
                ProjectileType.Knuppel => ItemType.CannonKnuppel,
                ProjectileType.Shrapnel => ItemType.CannonShrapnel,
                ProjectileType.Fishnet => ItemType.CannonFishnet,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}