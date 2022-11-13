using Ragon.Client;
using Ragon.Common;
using UnityEngine;

namespace Global.Services.Network.Instantiators.Runtime
{
    public class NetworkPayload : IRagonPayload
    {
        private int _id;
        private Vector2 _position;

        public int Id => _id;
        public Vector2 Position => _position;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteInt(_id);

            serializer.WriteFloat(_position.x);
            serializer.WriteFloat(_position.y);

            SerializeData(serializer);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            _id = serializer.ReadInt();
            _position = new Vector2(serializer.ReadFloat(), serializer.ReadFloat());

            DeserializeData(serializer);
        }

        public void SetData(int id, Vector2 position)
        {
            _id = id;
            _position = position;
        }

        protected virtual void SerializeData(RagonSerializer serializer)
        {
        }

        protected virtual void DeserializeData(RagonSerializer serializer)
        {
        }
    }
}