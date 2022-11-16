#region

using System;
using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    public interface IDroppedItem
    {
        int Id { get; }
        IItem Item { get; }

        void Construct(int id, Action<IDroppedItem> collectedCallback, IItem item);
        void Collect();
        void Destroy();
    }
}