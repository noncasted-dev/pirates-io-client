using GamePlay.Factions.Common;
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

        public PlayerPayload(
            string userName,
            ShipType shipType,
            FactionType faction)
        {
            _faction = faction;
            _userName = userName;
            _shipType = shipType;
        }

        private string _userName;
        private ShipType _shipType;
        private FactionType _faction;

        public string UserName => _userName;
        public ShipType ShipType => _shipType;
        public FactionType Faction => _faction;

        protected override void SerializeData(RagonSerializer serializer)
        {
            serializer.WriteString(_userName);
            serializer.WriteInt((int)_shipType);
            serializer.WriteInt((int)_faction);
        }

        protected override void DeserializeData(RagonSerializer serializer)
        {
            _userName = serializer.ReadString();
            _shipType = (ShipType)serializer.ReadInt();
            _faction = (FactionType)serializer.ReadInt();
        }
    }
}