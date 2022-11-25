using System;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Implementation.Items.Runtime
{
    public interface IDroppedItem
    {
        void Drop(int id,
            Action<IDroppedItem> collectedCallback,
            IItem item,
            Vector2 origin,
            Vector2 target);
        int Id { get; }
        IItem Item { get; }
        void Collect();
        void Destroy();
    }
}