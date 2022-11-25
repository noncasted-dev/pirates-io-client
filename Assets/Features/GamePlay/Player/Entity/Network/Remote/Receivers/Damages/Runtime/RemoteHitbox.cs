using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Common.Damages;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using Global.Services.Sounds.Runtime;
using Ragon.Client;
using UniRx;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime
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

        [SerializeField] private DamageConfigAsset _config;

        [SerializeField] private PlayerSpriteView _sprite;
        
        private IPlayerEventSender _eventSender;
        private IObjectProvider<AnimatedVfx> _explosion;

        private PlayerNetworkRoot _root;

        public bool IsLocal => _root.IsLocal;
        public string Id => _root.Entity.Owner.Id;

        public void ReceiveDamage(Damage damage, bool isProjectileLocal)
        {
            var damageEvent = new DamageEvent(damage);

            var explosion = _explosion.Get(damage.Origin);
            var direction = damage.Origin - (Vector2)_root.transform.position;
            direction.Normalize();
            explosion.transform.RotateAlong(direction);
            
            MessageBroker.Default.TriggerSound(PositionalSoundType.EnemyDamaged, damage.Origin);

            if (isProjectileLocal == true)
                _eventSender.ReplicateEvent(damageEvent);
        }

        private void OnDamageReceived(RagonPlayer player, DamageEvent damage)
        {
            _sprite.Flash(_config.FlashTime);
        }
    }
}