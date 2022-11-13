using System.Threading;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Player.Entity.Views.ShootPoint;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Implementation.Linear;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public class Shot : IUpdatable
    {
        public Shot(
            IUpdater updater,
            IProjectileReplicator projectileReplicator,
            IShooterConfig config,
            IObjectProvider<LinearProjectile> projectiles,
            IObjectProvider<AnimatedVfx> vfx,
            IShootPoint shootPoint,
            CancellationToken cancellation,
            float angle,
            float spread)
        {
            _updater = updater;
            _projectileReplicator = projectileReplicator;
            _config = config;
            _projectiles = projectiles;
            _shootPoint = shootPoint;
            _vfx = vfx;
            _cancellation = cancellation;
            _angle = angle;
            _spread = spread;
        }

        private readonly IUpdater _updater;
        private readonly IProjectileReplicator _projectileReplicator;
        private readonly IShooterConfig _config;
        private readonly IObjectProvider<LinearProjectile> _projectiles;
        private readonly IObjectProvider<AnimatedVfx> _vfx;
        private readonly IShootPoint _shootPoint;
        private readonly CancellationToken _cancellation;
        private readonly float _angle;
        private readonly float _spread;

        private float _time;
        private float[] _randomTimes;
        private bool[] _shotsRegistry;
        private int _shotCounter;

        public void Start()
        {
            _randomTimes = new float[_config.ShotsAmount];
            _shotsRegistry = new bool[_config.ShotsAmount];
            
            for (var i = 0; i < _randomTimes.Length; i++)
                _randomTimes[i] = _config.ShotsDelay + Random.Range(0f, _config.ShotRandomDelay);

            _updater.Add(this);
        }

        public void OnUpdate(float delta)
        {
            _time += delta;

            var halfSpread = _spread / 2f;
            
            for (var i = 0; i < _randomTimes.Length; i++)
            {
                if (_randomTimes[i] > _time || _shotsRegistry[i] == true)
                    continue;
                
                var randomAngle = Random.Range(-halfSpread, halfSpread);
                var resultAngle = _angle + randomAngle;
                var shootPosition = _shootPoint.GetShootPoint(resultAngle);
                var direction = AngleUtils.ToDirection(resultAngle);
                
                var projectile = _projectiles.Get(shootPosition);
                var additionalDistance = Random.Range(-_config.RandomDistance, _config.RandomDistance);
                var parameters = _config.CreateParams(additionalDistance);
                
                projectile.Fire(direction, parameters, true);
                _vfx.Get(shootPosition);

                _shotsRegistry[i] = true;
                _shotCounter++;
                
                _projectileReplicator.Replicate(
                    ProjectileType.Ordinary,
                    shootPosition,
                    resultAngle,
                    parameters.Speed,
                    parameters.Damage,
                    parameters.Distance);
            }

            if (_shotCounter == _config.ShotsAmount || _cancellation.IsCancellationRequested == true)
                _updater.Remove(this);
        }
    }
}