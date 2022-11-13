namespace GamePlay.Services.Projectiles.Mover.Abstract
{
    public interface IMovableProjectile
    {
        IProjectileMovement Movement { get; }
        IProjectileActions Actions { get; }
    }
}