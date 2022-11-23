using GamePlay.Player.Entity.Components.Definition;
using Global.Services.Network.Instantiators.Runtime;
using Ragon.Common;

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    public class PlayerPayload : NetworkPayload
    {
        public PlayerPayload()
        {
        }

        public PlayerPayload(string userName, ShipType shipType)
        {
            _userName = userName;
            _shipType = shipType;
        }

        private string _userName;
        private ShipType _shipType;

        public string UserName => _userName;
        public ShipType ShipType => _shipType;

        protected override void SerializeData(RagonSerializer serializer)
        {
            serializer.WriteString(_userName);
            serializer.WriteInt((int)_shipType);
        }

        protected override void DeserializeData(RagonSerializer serializer)
        {
            _userName = serializer.ReadString();
            _shipType = (ShipType)serializer.ReadInt();
        }
    }
}