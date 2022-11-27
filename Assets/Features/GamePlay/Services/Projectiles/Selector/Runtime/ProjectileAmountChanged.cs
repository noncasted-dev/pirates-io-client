namespace GamePlay.Services.Projectiles.Selector.Runtime
{
    public readonly struct ProjectileAmountChanged
    {
        public ProjectileAmountChanged(ProjectileType type, int amount)
        {
            _type = type;
            Amount = amount;
        }
        
        public readonly ProjectileType _type;
        public readonly int Amount;
    }
}