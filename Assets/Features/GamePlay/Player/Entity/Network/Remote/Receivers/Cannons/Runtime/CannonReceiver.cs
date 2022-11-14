using Common.Structs;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using Ragon.Client;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Cannons.Runtime
{
    public class CannonReceiver
    {
        private readonly ISpriteTransform _spriteTransform;
        private readonly RangeAttackConfigAsset _config;
        private readonly IProjectileReplicator _projectileReplicator;

        public CannonReceiver(
            ISpriteTransform spriteTransform,
            IPlayerEventListener listener,
            IProjectileReplicator projectileReplicator,
            RangeAttackConfigAsset config)
        {
            _spriteTransform = spriteTransform;
            _config = config;
            _projectileReplicator = projectileReplicator;
            
            listener.AddListener<ProjectileInstantiateEvent>(OnShoot);
        }

        private void OnShoot(RagonPlayer player, ProjectileInstantiateEvent data)
        {
            var direction = -AngleUtils.ToDirection(data.Angle);
            _spriteTransform.Impact(direction, _config.ImpactDistance, _config.ImpactTime);
            
            _projectileReplicator.Replicate(player.Id, data);
        }
    }
}