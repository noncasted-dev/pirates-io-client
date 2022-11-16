using System.Collections.Generic;
using GamePlay.Services.DroppedObjects.Implementation.Items.Runtime;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Presenter.Runtime
{
    public class DroppedObjectsStorage
    {
        private readonly Dictionary<int, IDroppedItem> _droppedItems = new();

        public void Add(int id, IDroppedItem dropped)
        {
            _droppedItems.Add(id, dropped);
        }

        public void Remove(int id)
        {
            if (_droppedItems.ContainsKey(id) == false)
            {
                Debug.LogError($"No item with id: {id} spawned");
                return;
            }

            _droppedItems[id].Destroy();
            _droppedItems.Remove(id);
        }
    }
}