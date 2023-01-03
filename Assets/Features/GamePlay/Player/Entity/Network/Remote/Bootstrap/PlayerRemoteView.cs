using System;
using Common.ObjectsPools.Runtime.Abstract;
using Common.VFX;
using GamePlay.Factions.Common;
using GamePlay.Player.Entity.Network.Remote.Receivers.Cannons.Runtime;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Player.Entity.Network.Remote.Receivers.Death.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Implementation.Dead;
using Global.Services.Updaters.Runtime.Abstract;
using Ragon.Client;
using TMPro;
using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Network.Remote.Bootstrap
{
    public class PlayerRemoteView : MonoBehaviour, IPoolObject<PlayerRemoteView>
    {
        public void Construct(
            ILogger logger,
            IUpdater updater,
            IProjectileReplicator projectileReplicator,
            IObjectProvider<AnimatedVfx> explosion,
            IObjectProvider<DeadShipVfx> deadShip,
            PlayerNetworkRoot networkRoot,
            FactionType faction)
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

            var _ = new HealthReceiver(_fireController, networkRoot, deadShip, transform);

            _hitbox.Construct(networkRoot, networkRoot, networkRoot, explosion, faction);
            
            foreach (var switchableCollider in _colliders)
                switchableCollider.enabled = true;
            
            networkRoot.SetDestroyCallback(OnDestroyedEntity);
            _nickName.text = networkRoot.Entity.GetSpawnPayload<PlayerPayload>().UserName;
        }

        [SerializeField] private PlayerSpriteTransform _spriteTransform;
        [SerializeField] private RangeAttackConfigAsset _config;
        [SerializeField] private RemoteHitbox _hitbox;
        [SerializeField] private PlayerSpriteView _spriteView;
        [SerializeField] private FireController _fireController;
        [SerializeField] private TMP_Text _nickName;

        [SerializeField] private Collider2D[] _colliders;
        
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

        private void OnDestroyedEntity()
        {
            _returnToPool?.Invoke(this);
        }
    }
}