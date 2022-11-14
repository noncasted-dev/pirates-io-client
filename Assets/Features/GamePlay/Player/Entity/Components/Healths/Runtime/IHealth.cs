namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public interface IHealth
    {
        bool IsAlive { get; }
        
        void Respawn(int health);
        void Heal(int add);
        void ApplyDamage(int damage);
    }
}