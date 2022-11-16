#region

using Ragon.Client;
using Ragon.Common;

#endregion

namespace GamePlay.Services.ObjectDroppers.Network.Runtime
{
    public class ItemCollectedEvent : IRagonEvent
    {
        public ItemCollectedEvent()
        {
        }

        public ItemCollectedEvent(int id)
        {
            _id = id;
        }

        private int _id;

        public int Id => _id;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteInt(_id);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            _id = serializer.ReadInt();
        }
    }
}