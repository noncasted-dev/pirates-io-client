using GamePlay.Common.Damages;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Services.Projectiles.Entity;
using GamePlay.Services.Projectiles.Replicator.Runtime;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Local.Replicators.Canons.Runtime
{
    public class CannonReplicator : ICannonReplicator
    {
        public CannonReplicator(
            IPlayerEventSender eventSender)
        {
            _eventSender = eventSender;
        }

        private readonly IPlayerEventSender _eventSender;

        public string PlayerId => RagonNetwork.Room.LocalPlayer.Id;

        public void Replicate(ProjectileType type, Vector2 position, float angle, float speed, int damage,
            float distance)
        {
            var data = new ProjectileInstantiateEvent
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
                RagonTarget.ExceptInvoker);
        }
    }
}