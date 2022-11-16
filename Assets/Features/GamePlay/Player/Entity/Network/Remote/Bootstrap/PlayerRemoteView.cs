#region

using System;
using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Player.Entity.Network.Remote.Receivers.Cannons.Runtime;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using Global.Services.Updaters.Runtime.Abstract;
using Ragon.Client;
using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

#endregion

namespace GamePlay.Player.Entity.Network.Remote.Bootstrap
{
    public class PlayerRemoteView : RagonBehaviour, IPoolObject<PlayerRemoteView>
    {
        public void Construct(
            ILogger logger,
            IUpdater updater,
            IProjectileReplicator projectileReplicator,
            IObjectProvider<AnimatedVfx> explosion,
            PlayerNetworkRoot networkRoot)
        {
            _spriteTransform.Construct(logger, updater);
            _spriteView.Construct(logger);
            _spriteView.OnAwake();

            _spriteTransform.OnAwake();

            var cannonReceiver = new CannonReceiver(
                _spriteTransform,
                networkRoot,
                projectileReplicator,
                _config);

            _hitbox.Construct(networkRoot, networkRoot, networkRoot, explosion);
        }

        [SerializeField] private PlayerSpriteTransform _spriteTransform;
        [SerializeField] private RangeAttackConfigAsset _config;
        [SerializeField] private RemoteHitbox _hitbox;
        [SerializeField] private PlayerSpriteView _spriteView;

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