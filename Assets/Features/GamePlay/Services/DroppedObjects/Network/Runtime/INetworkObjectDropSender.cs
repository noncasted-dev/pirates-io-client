using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Network.Runtime
{
    public interface INetworkObjectDropSender
    {
        void OnItemDropped(ItemType type, int count, Vector2 origin, Vector2 target);
        void OnItemCollected(int id);
    }
}