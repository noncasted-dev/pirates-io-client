using Ragon.Common;
using UnityEngine;

namespace Common.RagonUtils
{
    public static class SerializerUtils
    {
        public static void WriteVector(this RagonSerializer serializer, Vector2 value)
        {
            serializer.WriteFloat(value.x);
            serializer.WriteFloat(value.y);
        }

        public static Vector2 ReadVector(this RagonSerializer serializer)
        {
            return new Vector2(serializer.ReadFloat(), serializer.ReadFloat());
        }
    }
}