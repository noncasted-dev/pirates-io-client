#region

using Common.RagonUtils;
using GamePlay.Common.Damages;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;

#endregion

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
        }

        private int _amount;
        private Vector2 _origin;

        public int Amount => _amount;
        public Vector2 Origin => _origin;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteInt(_amount);
            serializer.WriteVector(_origin);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            _amount = serializer.ReadInt();
            _origin = serializer.ReadVector();
        }
    }
}