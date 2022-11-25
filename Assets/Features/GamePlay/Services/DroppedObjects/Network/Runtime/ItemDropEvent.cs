using Common.RagonUtils;
using GamePlay.Items.Abstract;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Network.Runtime
{
    public class ItemDropEvent : IRagonEvent
    {
        public ItemDropEvent()
        {
        }

        public ItemDropEvent(
            ItemType type,
            Vector2 origin,
            Vector2 target,
            int count,
            int id)
        {
            _target = target;
            _type = type;
            _origin = origin;
            _count = count;
            _id = id;
        }

        private int _count;
        private int _id;

        private Vector2 _origin;
        private Vector2 _target;

        private ItemType _type;

        public Vector2 Origin => _origin;
        public Vector2 Target => _target;
        public ItemType Type => _type;
        public int Count => _count;
        public int Id => _id;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteVector(_origin);
            serializer.WriteVector(_target);
            serializer.WriteInt((int)_type);
            serializer.WriteInt(_count);
            serializer.WriteInt(_id);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            _origin = serializer.ReadVector();
            _target = serializer.ReadVector();
            _type = (ItemType)serializer.ReadInt();
            _count = serializer.ReadInt();
            _id = serializer.ReadInt();
        }
    }
}