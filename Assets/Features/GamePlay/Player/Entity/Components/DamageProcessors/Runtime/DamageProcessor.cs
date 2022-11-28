using System;
using Common.ObjectsPools.Runtime.Abstract;
using Common.Structs;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.VFX.Pool.Implementation.Animated;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.Updaters.Runtime.Abstract;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.DamageProcessors.Runtime
{
    public class DamageProcessor : IUpdatable
    {
        public DamageProcessor(
            IHealth health,
            ISail sail,
            IDeath death,
            IPlayerEventListener listener,
            ISpriteFlash flash,
            IVfxPoolProvider vfxPool,
            IBodyTransform transform,
            IUpdater updater,
            IShipResources resources,
            DamageConfigAsset config)
        {
            _updater = updater;
            _resources = resources;
            _health = health;
            _sail = sail;
            _death = death;
            _flash = flash;
            _transform = transform;
            _config = config;
            _explosion = vfxPool.GetPool<AnimatedVfx>(_config.Explosion);

            listener.AddListener<DamageEvent>(OnDamageReceived);
        }

        private const float _shallowTickDuration = 3f;
        
        private readonly DamageConfigAsset _config;
        private readonly IDeath _death;
        private readonly IObjectProvider<AnimatedVfx> _explosion;
        private readonly ISpriteFlash _flash;

        private readonly IHealth _health;
        private readonly ISail _sail;
        private readonly IBodyTransform _transform;
        private readonly IUpdater _updater;
        private readonly IShipResources _resources;

        private float _shallowTime;
        
        private void OnDamageReceived(RagonPlayer player, DamageEvent damage)
        {
            _health.ApplyDamage(damage.Amount);

            var explosion = _explosion.Get(damage.Origin);
            var direction = damage.Origin - _transform.Position;
            direction.Normalize();
            explosion.transform.RotateAlong(direction);

            switch (damage.Type)
            {
                case ProjectileType.Ball:
                    _sail.Damage(0.2f);
                    break;
                case ProjectileType.Knuppel:
                    _sail.Damage(1f);
                    break;
                case ProjectileType.Shrapnel:
                    _sail.Damage(0.1f);
                    break;
                case ProjectileType.Fishnet:
                    break;
                default:
                    Debug.Log($"Unrecognized projectile type: {damage.Type}");
                    break;
            }

            if (_health.IsAlive == false)
                _death.Enter();
            else
                _flash.Flash(_config.FlashTime);
        }

        public void OnShallowEntered()
        {
            _updater.Add(this);
        }

        public void OnShallowExited()
        {
            _updater.Remove(this);
        }

        public void OnUpdate(float delta)
        {
            _shallowTime += delta;
            
            if (_shallowTime < _shallowTickDuration)
                return;

            _shallowTime = 0f;
            
            _health.ApplyDamage(_resources.ShallowDamage);
            
            if (_health.IsAlive == false)
                _death.Enter();
            else
                _flash.Flash(_config.FlashTime);
        }
    }
}