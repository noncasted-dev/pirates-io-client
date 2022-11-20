namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public interface IHealth
    {
        int Amount { get; }
        bool IsAlive { get; }

        void Respawn(int health);
        void Heal(int add);
        void ApplyDamage(int damage);
    }
}