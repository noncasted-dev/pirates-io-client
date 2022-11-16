#region

using Common.RagonUtils;
using GamePlay.Items.Abstract;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;

#endregion

namespace GamePlay.Services.ObjectDroppers.Network.Runtime
{
    public class ItemDropEvent : IRagonEvent
    {
        public ItemDropEvent()
        {
        }

        public ItemDropEvent(ItemType type, Vector2 position, int count, int id)
        {
            _type = type;
            _position = position;
            _count = count;
            _id = id;
        }

        private Vector2 _position;
        private ItemType _type;
        private int _count;
        private int _id;

        public Vector2 Position => _position;
        public ItemType Type => _type;
        public int Count => _count;
        public int Id => _id;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteVector(_position);
            serializer.WriteInt((int)_type);
            serializer.WriteInt(_count);
            serializer.WriteInt(_id);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            _position = serializer.ReadVector();
            _type = (ItemType)serializer.ReadInt();
            _count = serializer.ReadInt();
            _id = serializer.ReadInt();
        }
    }
}