﻿using System.Threading;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Common.Damages;
using GamePlay.Player.Entity.Network.Local.Replicators.Canons.Runtime;
using GamePlay.Player.Entity.Views.ShootPoint;
using GamePlay.Services.Projectiles.Implementation.Linear.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using Global.Services.Sounds.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public class Shot : IUpdatable
    {
        public Shot(
            IUpdater updater,
            ICannonReplicator cannonReplicator,
            IShooterConfig config,
            IObjectProvider<LinearProjectile> projectiles,
            IObjectProvider<AnimatedVfx> vfx,
            IShootPoint shootPoint,
            CancellationToken cancellation,
            ProjectileType type,
            float angle,
            float spread)
        {
            _updater = updater;
            _cannonReplicator = cannonReplicator;
            _config = config;
            _projectiles = projectiles;
            _shootPoint = shootPoint;
            _vfx = vfx;
            _cancellation = cancellation;
            _type = type;
            _angle = angle;
            _spread = spread;
        }

        private readonly float _angle;
        private readonly CancellationToken _cancellation;
        private readonly ICannonReplicator _cannonReplicator;
        private readonly IShooterConfig _config;
        private readonly IProjectileReplicator _projectileReplicator;
        private readonly IObjectProvider<LinearProjectile> _projectiles;
        private readonly IShootPoint _shootPoint;
        private readonly float _spread;
        private readonly ProjectileType _type;

        private readonly IUpdater _updater;
        private readonly IObjectProvider<AnimatedVfx> _vfx;
        private bool _isCanceled = false;
        private float[] _randomTimes;
        private int _shotCounter;
        private int _shotsCount;
        private bool[] _shotsRegistry;

        private float _time;

        public void OnUpdate(float delta)
        {
            if (_isCanceled == true)
            {
                _updater.Remove(this);
                return;
            }

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

                projectile.Fire(direction, parameters, true, _cannonReplicator.PlayerId);
                var vfx = _vfx.Get(shootPosition);
                vfx.transform.rotation = Quaternion.Euler(0f, 0f, resultAngle);

                _shotsRegistry[i] = true;
                _shotCounter++;

                _cannonReplicator.Replicate(
                    _type,
                    shootPosition,
                    resultAngle,
                    parameters.Speed,
                    parameters.Damage,
                    parameters.Distance);

                if (i == 0)
                    MessageBrokerSoundExtensions.TriggerSound(PositionalSoundType.CannonBallShot, shootPosition);
            }

            if (_shotCounter == _shotsCount || _cancellation.IsCancellationRequested == true)
                _updater.Remove(this);
        }

        public void Start(int shotsCount)
        {
            _shotsCount = shotsCount;
            _randomTimes = new float[shotsCount];
            _shotsRegistry = new bool[shotsCount];

            for (var i = 0; i < _randomTimes.Length; i++)
                _randomTimes[i] = _config.ShotsDelay + Random.Range(0f, _config.ShotRandomDelay);

            _updater.Add(this);
        }

        public void Cancel()
        {
            _isCanceled = true;
        }
    }
}