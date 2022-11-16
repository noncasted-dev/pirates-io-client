#region

using Unity.Collections;
using Unity.Jobs;

#endregion

namespace GamePlay.Services.Projectiles.Mover
{
    public struct MoveJob : IJobParallelFor
    {
        public MoveJob(NativeArray<ProjectileMoveData> projectiles, float delta)
        {
            _projectiles = projectiles;
            _delta = delta;
        }

        private NativeArray<ProjectileMoveData> _projectiles;
        private readonly float _delta;
        public NativeArray<ProjectileMoveData> Projectiles => _projectiles;

        public void Execute(int index)
        {
            var projectile = _projectiles[index];

            var move = projectile.Direction * projectile.Speed * _delta;
            var newPosition = projectile.CurrentPosition + move;

            projectile.SetPosition(newPosition);

            _projectiles[index] = projectile;
        }
    }
}