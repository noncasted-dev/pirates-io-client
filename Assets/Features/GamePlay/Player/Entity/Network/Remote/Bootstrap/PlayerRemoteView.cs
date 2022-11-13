using System;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Player.Entity.Network.Remote.Receivers.Cannons.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.Projectiles.Factory;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using Ragon.Client;
using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Network.Remote.Bootstrap
{
    public class PlayerRemoteView : RagonBehaviour, IPoolObject<PlayerRemoteView>
    {
        [SerializeField] private PlayerSpriteTransform _spriteTransform;
        [SerializeField] private RangeAttackConfigAsset _config;
        
        public void Construct(
            ILogger logger,
            IUpdater updater,
            IProjectileReplicator projectileReplicator,
            PlayerNetworkRoot networkRoot)
        {
            _spriteTransform.Construct(logger, updater);

            var cannonReceiver = new CannonReceiver(
                _spriteTransform as ISpriteTransform,
                networkRoot,
                projectileReplicator,
                _config);
        }

        private Action<PlayerRemoteView> _returnToPool;

        public GameObject GameObject => gameObject;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetupPoolObject(Action<PlayerRemoteView> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        public void OnTaken()
        {
        }

        public void OnReturned()
        {
        }

        public override void OnDestroyedEntity()
        {
            _returnToPool?.Invoke(this);
        }
    }
}