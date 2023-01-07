using System;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    public interface IDroppedItem
    {
        int Id { get; }
        IItem Item { get; }

        void Drop(int id,
            Action<IDroppedItem> collectedCallback,
            IItem item,
            Vector2 origin,
            Vector2 target);

        void Collect();
        void Destroy();
    }
}