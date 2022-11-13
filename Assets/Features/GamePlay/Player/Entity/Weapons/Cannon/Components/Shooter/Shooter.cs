using System.Threading;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.ShootPoint;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Implementation.Linear;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public class Shooter : IShooter, IAwakeCallback
    {
        public Shooter(
            IShooterConfig config,
            IProjectilesPoolProvider projectilesPoolProvider,
            IShootPoint shootPoint,
            IVfxPoolProvider vfxPoolProvider)
        {
            _config = config;
            _projectilesPoolProvider = projectilesPoolProvider;
            _shootPoint = shootPoint;
            _vfxPoolProvider = vfxPoolProvider;
        }

        private readonly IShooterConfig _config;
        private readonly IProjectilesPoolProvider _projectilesPoolProvider;
        private readonly IShootPoint _shootPoint;
        private readonly IVfxPoolProvider _vfxPoolProvider;

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

            Process(angle, spread).Forget();
        }

        private async UniTaskVoid Process(float angle, float spread)
        {
            var halfSpread = spread / 2f;
            for (var i = 0; i < _config.ShotsAmount; i++)
            {
                var randomAngle = Random.Range(-halfSpread, halfSpread);
                var resultAngle = angle + randomAngle;
                var shootPosition = _shootPoint.GetShootPoint(resultAngle);
                var direction = AngleUtils.ToDirection(resultAngle);

                var projectile = _projectiles.Get(shootPosition);

                projectile.Fire(shootPosition, direction, _config.CreateParams());
                _vfx.Get(shootPosition);

                var delay = (int)(_config.ShotsDelay * 1000f);

                await UniTask.Delay(delay, false, PlayerLoopTiming.Update, _cancellation.Token);
            }
        }
    }
}