using GamePlay.Common.Damages;

namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public readonly struct ProjectileAmountChangedEvent
    {
        public ProjectileAmountChangedEvent(ProjectileType type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public readonly ProjectileType Type;
        public readonly int Amount;
    }
}