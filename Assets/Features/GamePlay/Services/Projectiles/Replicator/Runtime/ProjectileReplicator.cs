using System.Collections.Generic;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Services.Network.Service.Common.EntityProvider.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Implementation.Linear;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Local.Services.Abstract.Callbacks;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    public class ProjectileReplicator :
        MonoBehaviour,
        ILocalLoadListener,
        ILocalAwakeListener,
        IProjectileReplicator
    {
        [Inject]
        private void Construct(
            IPlayerPositionProvider positionProvider,
            INetworkSessionEventSender eventSender,
            INetworkSessionEventListener eventListener,
            IProjectilesPoolProvider projectilesPoolProvider,
            IVfxPoolProvider vfxPoolProvider,
            ProjectileReplicatorConfigAsset config)
        {
            _vfxPoolProvider = vfxPoolProvider;
            _projectilesPoolProvider = projectilesPoolProvider;
            _eventListener = eventListener;
            _eventSender = eventSender;
            _config = config;
            _positionProvider = positionProvider;
        }

        private readonly Dictionary<ProjectileType, IObjectProvider<LinearProjectile>> _projectiles = new();

        private IPlayerPositionProvider _positionProvider;
        private INetworkSessionEventSender _eventSender;
        private INetworkSessionEventListener _eventListener;

        private ProjectileReplicatorConfigAsset _config;
        private IProjectilesPoolProvider _projectilesPoolProvider;
        private IVfxPoolProvider _vfxPoolProvider;
        private IObjectProvider<AnimatedVfx> _vfx;

        public void OnLoaded()
        {
            _eventListener.AddListener<ProjectileInstantiateEvent>(OnProjectileReceived);
        }
        
        public void OnAwake()
        {
            foreach (var projectile in _config.Projectiles)
            {
                var pool =
                    _projectilesPoolProvider.GetPool<LinearProjectile>(projectile.Value);
                
                _projectiles.Add(projectile.Key, pool);
            }

            _vfx = _vfxPoolProvider.GetPool<AnimatedVfx>(_config.Vfx);
        }

        public void Replicate(
            ProjectileType type,
            Vector2 position,
            float angle,
            float speed,
            int damage,
            float distance)
        {
            var data = new ProjectileInstantiateEvent()
            {
                Type = type,
                Angle = angle,
                Damage = damage,
                Position = position,
                Speed = speed,
                Distance = distance
            };

            _eventSender.ReplicateEvent(
                data,
                RagonTarget.All,
                RagonReplicationMode.Server);
        }

        private void OnProjectileReceived(RagonPlayer player, ProjectileInstantiateEvent data)
        {
            if (player.IsMe == true)
            {
                Debug.Log("Replicated back");
                return;
            }
            
            var distance = Vector2.Distance(data.Position, _positionProvider.Position);
            
            if (distance > _config.ReplicateDistance)
                return;
            
            var direction = AngleUtils.ToDirection(data.Angle);
            
            var projectile = _projectiles[data.Type].Get(data.Position);
            
            var parameters = new ShootParams(data.Damage, data.Speed, data.Distance);
                
            projectile.Fire(direction, parameters, true);
            _vfx.Get(data.Position);
        }
    }
}