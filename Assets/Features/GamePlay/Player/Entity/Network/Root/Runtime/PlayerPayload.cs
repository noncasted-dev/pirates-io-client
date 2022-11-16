#region

using Global.Services.Network.Instantiators.Runtime;
using Ragon.Common;

#endregion

namespace GamePlay.Player.Entity.Network.Root.Runtime
{
    public class PlayerPayload : NetworkPayload
    {
        public PlayerPayload()
        {
        }

        public PlayerPayload(string userName)
        {
            _userName = userName;
        }

        private string _userName;

        public string UserName => _userName;

        protected override void SerializeData(RagonSerializer serializer)
        {
            serializer.WriteString(_userName);
        }

        protected override void DeserializeData(RagonSerializer serializer)
        {
            _userName = serializer.ReadString();
        }
    }
}