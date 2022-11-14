using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Common.Damages;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using Ragon.Client;
using UnityEngine;

namespace Features.GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime
{
    public class RemoteHitbox : MonoBehaviour, IDamageReceiver
    {
        public void Construct(
            PlayerNetworkRoot root,
            IPlayerEventSender eventSender,
            IPlayerEventListener eventListener,
            IObjectProvider<AnimatedVfx> explosion)
        {
            _explosion = explosion;
            _eventSender = eventSender;
            _root = root;
            
            eventListener.AddListener<DamageEvent>(OnDamageReceived);
        }

        [SerializeField] private PlayerSpriteView _sprite;
        [SerializeField] private DamageConfigAsset _config;
        
        private PlayerNetworkRoot _root;
        private IPlayerEventSender _eventSender;
        private IObjectProvider<AnimatedVfx> _explosion;

        public bool IsLocal => _root.IsLocal;
        
        public void ReceiveDamage(Damage damage, bool isProjectileLocal)
        {
            var damageEvent = new DamageEvent(damage);

            var explosion = _explosion.Get(damage.Origin);
            var direction = damage.Origin - (Vector2)_root.transform.position;
            direction.Normalize();
            explosion.transform.RotateAlong(direction);
            
            if (isProjectileLocal == true)
                _eventSender.ReplicateEvent(damageEvent);
        }

        private void OnDamageReceived(RagonPlayer player, DamageEvent damage)
        {
            _sprite.Flash(_config.FlashTime);
        }
    }
}