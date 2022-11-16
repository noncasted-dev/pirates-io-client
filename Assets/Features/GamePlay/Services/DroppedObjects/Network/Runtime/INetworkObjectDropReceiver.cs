using System;

namespace GamePlay.Services.DroppedObjects.Network.Runtime
{
    public interface INetworkObjectDropReceiver
    {
        event Action<ItemDropEvent> ItemDropped;
        event Action<int> ItemCollected;
    }
}