using Unity.Collections;
using Unity.Jobs;

namespace GamePlay.Services.Projectiles.Mover
{
    public struct MoveJob : IJobParallelFor
    {
        public MoveJob(NativeArray<LinearProjectileData> projectiles)
        {
            _projectiles = projectiles;
        }

        private NativeArray<LinearProjectileData> _projectiles;
        public NativeArray<LinearProjectileData> Projectiles => _projectiles;

        public void Execute(int index)
        {
            var projectile = _projectiles[index];

            var newPosition = projectile.CurrentPosition + projectile.Direction * projectile.Speed;
            projectile.SetPosition(newPosition);

            _projectiles[index] = projectile;
        }
    }
}