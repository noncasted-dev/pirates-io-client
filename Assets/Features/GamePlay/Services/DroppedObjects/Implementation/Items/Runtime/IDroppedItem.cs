using System;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    public interface IDroppedItem
    {
        void Construct(int id, Action<IDroppedItem> collectedCallback, IItem item);
        int Id { get; }
        IItem Item { get; }
        void Collect();
        void Destroy();
    }
}