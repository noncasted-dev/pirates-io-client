#region

using Common.Structs;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using Ragon.Client;

#endregion

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Cannons.Runtime
{
    public class CannonReceiver
    {
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

        private readonly RangeAttackConfigAsset _config;
        private readonly IProjectileReplicator _projectileReplicator;
        private readonly ISpriteTransform _spriteTransform;

        private void OnShoot(RagonPlayer player, ProjectileInstantiateEvent data)
        {
            var direction = -AngleUtils.ToDirection(data.Angle);
            _spriteTransform.Impact(direction, _config.ImpactDistance, _config.ImpactTime);

            _projectileReplicator.Replicate(player.Id, data);
        }
    }
}