namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    public readonly struct PlayerRespawnedEvent
    {
        public PlayerRespawnedEvent(int health, int maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }

        public readonly int Health;
        public readonly int MaxHealth;
    }
}