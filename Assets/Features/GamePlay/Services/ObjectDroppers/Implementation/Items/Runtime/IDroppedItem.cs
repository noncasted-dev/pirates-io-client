#region

using System;
using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.ObjectDroppers.Implementation.Items.Runtime
{
    public interface IDroppedItem
    {
        int Id { get; }

        void Construct(int id, Action<IDroppedItem> collectedCallback, IItem item);
        void Collect();
        void Destroy();
    }
}