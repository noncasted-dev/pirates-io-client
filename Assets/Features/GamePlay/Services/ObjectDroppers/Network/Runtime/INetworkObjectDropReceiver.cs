#region

using System;

#endregion

namespace GamePlay.Services.ObjectDroppers.Network.Runtime
{
    public interface INetworkObjectDropReceiver
    {
        event Action<ItemDropEvent> ItemDropped;
        event Action<int> ItemCollected;
    }
}