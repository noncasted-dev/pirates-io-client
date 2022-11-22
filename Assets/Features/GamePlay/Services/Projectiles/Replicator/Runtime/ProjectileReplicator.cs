using System.Collections.Generic;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Implementation.Linear.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    public class ProjectileReplicator :
        MonoBehaviour,
        ILocalBootstrappedListener,
        IProjectileReplicator
    {
        [Inject]
        private void Construct(
            IPlayerPositionProvider positionProvider,
            IProjectilesPoolProvider projectilesPoolProvider,
            IVfxPoolProvider vfxPoolProvider,
            ProjectileReplicatorConfigAsset config)
        {
            _vfxPoolProvider = vfxPoolProvider;
            _projectilesPoolProvider = projectilesPoolProvider;
            _config = config;
            _positionProvider = positionProvider;
        }

        private readonly Dictionary<ProjectileType, IObjectProvider<LinearProjectile>> _projectiles = new();

        private ProjectileReplicatorConfigAsset _config;

        private IPlayerPositionProvider _positionProvider;
        private IProjectilesPoolProvider _projectilesPoolProvider;
        private IObjectProvider<AnimatedVfx> _vfx;
        private IVfxPoolProvider _vfxPoolProvider;

        public void OnBootstrapped()
        {
            foreach (var projectile in _config.Projectiles)
            {
                var pool =
                    _projectilesPoolProvider.GetPool<LinearProjectile>(projectile.Value);

                _projectiles.Add(projectile.Key, pool);
            }

            _vfx = _vfxPoolProvider.GetPool<AnimatedVfx>(_config.Fire);
        }

        public void Replicate(string creatorId, ProjectileInstantiateEvent data)
        {
            var distance = Vector2.Distance(data.Position, _positionProvider.Position);

            if (distance > _config.ReplicateDistance)
                return;

            var direction = AngleUtils.ToDirection(data.Angle);

            var projectile = _projectiles[data.Type].Get(data.Position);

            var parameters = new ShootParams(data.Damage, data.Speed, data.Distance);

            projectile.Fire(direction, parameters, true, creatorId);
            var vfx = _vfx.Get(data.Position);
            vfx.transform.rotation = Quaternion.Euler(0f, 0f, data.Angle);
        }
    }
}