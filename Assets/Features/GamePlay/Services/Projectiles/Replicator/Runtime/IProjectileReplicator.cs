namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    public interface IProjectileReplicator
    {
        void Replicate(string creatorId, ProjectileInstantiateEvent data);
    }
}