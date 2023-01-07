using Common.RagonUtils;
using GamePlay.Common.Damages;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime
{
    public class DamageEvent : IRagonEvent
    {
        public DamageEvent()
        {
        }

        public DamageEvent(Damage damage)
        {
            _origin = damage.Origin;
            _amount = damage.Amount;
            _type = damage.Type;
        }

        private int _amount;
        private Vector2 _origin;
        private ProjectileType _type;

        public int Amount => _amount;
        public Vector2 Origin => _origin;
        public ProjectileType Type => _type;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteInt(_amount);
            serializer.WriteVector(_origin);
            serializer.WriteInt((int)_type);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            _amount = serializer.ReadInt();
            _origin = serializer.ReadVector();
            _type = (ProjectileType)serializer.ReadInt();
        }
    }
}