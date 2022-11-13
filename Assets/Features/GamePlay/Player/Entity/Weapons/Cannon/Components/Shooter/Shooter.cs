using System.Threading;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.ShootPoint;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Implementation.Linear;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.Updaters.Runtime.Abstract;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public class Shooter : IShooter, IAwakeCallback
    {
        public Shooter(
            IUpdater updater,
            IShooterConfig config,
            IProjectilesPoolProvider projectilesPoolProvider,
            IShootPoint shootPoint,
            IVfxPoolProvider vfxPoolProvider,
            IProjectileReplicator projectileReplicator)
        {
            _updater = updater;
            _config = config;
            _projectilesPoolProvider = projectilesPoolProvider;
            _shootPoint = shootPoint;
            _vfxPoolProvider = vfxPoolProvider;
            _projectileReplicator = projectileReplicator;
        }

        private readonly IUpdater _updater;
        private readonly IShooterConfig _config;
        private readonly IProjectilesPoolProvider _projectilesPoolProvider;
        private readonly IShootPoint _shootPoint;
        private readonly IVfxPoolProvider _vfxPoolProvider;
        private readonly IProjectileReplicator _projectileReplicator;

        private IObjectProvider<LinearProjectile> _projectiles;
        private IObjectProvider<AnimatedVfx> _vfx;
        private CancellationTokenSource _cancellation;

        public void OnAwake()
        {
            _projectiles = _projectilesPoolProvider.GetPool<LinearProjectile>(_config.Projectile);
            _vfx = _vfxPoolProvider.GetPool<AnimatedVfx>(_config.Vfx);
        }

        public void Cancel()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;
        }

        public void Shoot(float angle, float spread)
        {
            Cancel();

            _cancellation = new CancellationTokenSource();

            var shot = new Shot(
                _updater,
                _projectileReplicator,
                _config,
                _projectiles,
                _vfx,
                _shootPoint,
                _cancellation.Token,
                angle,
                spread);

            shot.Start();
        }
    }
}