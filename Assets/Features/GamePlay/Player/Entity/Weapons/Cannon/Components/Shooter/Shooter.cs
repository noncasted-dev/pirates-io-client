﻿using System;
using System.Threading;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Common.Damages;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Network.Local.Replicators.Canons.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.ShootPoint;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Implementation.Linear.Runtime;
using GamePlay.Services.Projectiles.Selector.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    public class Shooter : IShooter, IPlayerAwakeListener, IPlayerDestroyListener
    {
        public Shooter(
            IUpdater updater,
            IShooterConfig config,
            ICannonReplicator cannonReplicator,
            IProjectilesPoolProvider projectilesPoolProvider,
            IShootPoint shootPoint,
            IVfxPoolProvider vfxPoolProvider,
            IProjectileSelector selector,
            IPlayerCargoStorage cargo,
            IShipResources resources)
        {
            _resources = resources;
            _updater = updater;
            _config = config;
            _cannonReplicator = cannonReplicator;
            _projectilesPoolProvider = projectilesPoolProvider;
            _shootPoint = shootPoint;
            _vfxPoolProvider = vfxPoolProvider;
            _selector = selector;
            _cargo = cargo;
        }

        private readonly ICannonReplicator _cannonReplicator;
        private readonly IPlayerCargoStorage _cargo;
        private readonly IShooterConfig _config;
        private readonly IProjectilesPoolProvider _projectilesPoolProvider;
        private readonly IShipResources _resources;
        private readonly IProjectileSelector _selector;
        private readonly IShootPoint _shootPoint;

        private readonly IUpdater _updater;
        private readonly IVfxPoolProvider _vfxPoolProvider;

        private IObjectProvider<LinearProjectile> _ball;
        private CancellationTokenSource _cancellation;

        private Shot _current;
        private IObjectProvider<LinearProjectile> _fishnet;
        private IObjectProvider<LinearProjectile> _knuppel;
        private IObjectProvider<LinearProjectile> _shrapnel;
        private IObjectProvider<AnimatedVfx> _vfx;

        public void OnAwake()
        {
            _ball = _projectilesPoolProvider.GetPool<LinearProjectile>(_config.Ball);
            _knuppel = _projectilesPoolProvider.GetPool<LinearProjectile>(_config.Knuppel);
            _shrapnel = _projectilesPoolProvider.GetPool<LinearProjectile>(_config.Shrapnel);
            _fishnet = _projectilesPoolProvider.GetPool<LinearProjectile>(_config.Fishnet);

            _vfx = _vfxPoolProvider.GetPool<AnimatedVfx>(_config.Vfx);
        }

        public void OnDestroyed()
        {
            Cancel();
        }

        public void Cancel()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;

            _current?.Cancel();
            _current = null;
        }

        public void Shoot(float angle, float spread)
        {
            Cancel();

            _cancellation = new CancellationTokenSource();

            var provider = _selector.Selected switch
            {
                ProjectileType.Ball => _ball,
                ProjectileType.Knuppel => _knuppel,
                ProjectileType.Shrapnel => _shrapnel,
                ProjectileType.Fishnet => _fishnet,
                _ => throw new ArgumentOutOfRangeException()
            };

            var shots = Mathf.Clamp(_resources.Cannons, 0, _resources.MaxCannons);

            if (shots < 1)
                shots = 1;

            var item = _selector.Selected.ConvertToItemType();

            if (_selector.Selected != ProjectileType.Fishnet)
            {
                if (shots > _cargo.Items[item].Count)
                    shots = _cargo.Items[item].Count;

                var reduceCount = (int)(shots / 10f);
                if (reduceCount < 1)
                    reduceCount = 1;

                _cargo.Reduce(item, reduceCount);
            }
            else
            {
                shots = 1;
            }

            _current = new Shot(
                _updater,
                _cannonReplicator,
                _config,
                provider,
                _vfx,
                _shootPoint,
                _cancellation.Token,
                _selector.Selected,
                angle,
                spread);

            _current.Start(shots);
        }

        public void Shoot(float angle, float spread, int count)
        {
            Cancel();

            _cancellation = new CancellationTokenSource();

            var provider = _ball;

            _current = new Shot(
                _updater,
                _cannonReplicator,
                _config,
                provider,
                _vfx,
                _shootPoint,
                _cancellation.Token,
                ProjectileType.Ball,
                angle,
                spread);

            _current.Start(count);
        }
    }
}