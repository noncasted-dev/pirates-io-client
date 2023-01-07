using GamePlay.Common.Damages;

namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public readonly struct ProjectileSelectedEvent
    {
        public ProjectileSelectedEvent(ProjectileType type)
        {
            Type = type;
        }

        public readonly ProjectileType Type;
    }
}