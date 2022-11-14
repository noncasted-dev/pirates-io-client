using GamePlay.Services.Projectiles.Entity;
using Ragon.Client;
using Ragon.Common;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Replicator.Runtime
{
    public class ProjectileInstantiateEvent : IRagonEvent
    {
        public float Angle;
        public int Damage;
        public float Distance;
        public Vector2 Position;
        public float Speed;
        public ProjectileType Type;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteUShort((ushort)Type);
            serializer.WriteFloat(Position.x);
            serializer.WriteFloat(Position.y);
            serializer.WriteFloat(Angle);
            serializer.WriteFloat(Speed);
            serializer.WriteInt(Damage);
            serializer.WriteFloat(Distance);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            Type = (ProjectileType)serializer.ReadUShort();
            Position = new Vector2(serializer.ReadFloat(), serializer.ReadFloat());
            Angle = serializer.ReadFloat();
            Speed = serializer.ReadFloat();
            Damage = serializer.ReadInt();
            Distance = serializer.ReadFloat();
        }
    }
}