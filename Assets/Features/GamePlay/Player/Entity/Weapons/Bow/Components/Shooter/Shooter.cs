using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Weapons.Bow.Views.ShootPoint;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Implementation.Linear;
using Global.Services.InputViews.Runtime;

namespace GamePlay.Player.Entity.Weapons.Bow.Components.Shooter
{
    public class Shooter : IShooter, IAwakeCallback
    {
        public Shooter(
            IShooterConfig config,
            IProjectilesPoolProvider poolProvider,
            IShootPoint shootPoint,
            IInputView inputView)
        {
            _config = config;
            _poolProvider = poolProvider;
            _shootPoint = shootPoint;
            _inputView = inputView;
        }

        private readonly IShooterConfig _config;
        private readonly IInputView _inputView;
        private readonly IProjectilesPoolProvider _poolProvider;
        private readonly IShootPoint _shootPoint;

        private IObjectProvider<LinearProjectile> _pool;

        public void OnAwake()
        {
            _pool = _poolProvider.GetPool<LinearProjectile>(_config.Reference);
        }

        public void Shoot(float angle)
        {
            var shootPosition = _shootPoint.GetShootPoint();

            var projectile = _pool.Get(shootPosition);

            var direction = AngleUtils.ToDirection(angle);
            projectile.Fire(shootPosition, direction, _config.CreateParams());
        }
    }
}