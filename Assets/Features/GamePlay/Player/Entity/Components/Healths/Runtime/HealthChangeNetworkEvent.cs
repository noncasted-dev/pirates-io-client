using Ragon.Client;
using Ragon.Common;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public class HealthChangeNetworkEvent : IRagonEvent
    {
        public int Current;
        public int Max;

        public void Serialize(RagonSerializer serializer)
        {
            serializer.WriteInt(Current);
            serializer.WriteInt(Max);
        }

        public void Deserialize(RagonSerializer serializer)
        {
            Current = serializer.ReadInt();
            Max = serializer.ReadInt();
        }
    }
}