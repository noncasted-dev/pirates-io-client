using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Items.Abstract
{
    public class ItemsVault
    {
        private readonly Dictionary<ItemType, IItem> _items = new();

        public IReadOnlyDictionary<ItemType, IItem> Copy()
        {
            var vaultCopy = new Dictionary<ItemType, IItem>();
            
            foreach (var item in _items)
            {
                var itemCopy = item.Value.Copy();
                vaultCopy.Add(item.Key, itemCopy);
            }

            return vaultCopy;
        }

        public void Set(IReadOnlyDictionary<ItemType, IItem> from)
        {
            _items.Clear();

            foreach (var (key, value) in from)
                _items.Add(key, value);
        }

        public void Add(IItem item)
        {
            var type = item.BaseData.Type;
            
            if (_items.ContainsKey(type) == true)
            {
                _items[type].Add(item.Count);
                return;
            }

            _items.Add(type, item);
        }

        public void Reduce(IItem item, int amount)
        {
            var type = item.BaseData.Type;

            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type} found in vault.");
                return;
            }
            
            _items[type].Reduce(amount);
        }

        public void Delete(IItem item)
        {
            var type = item.BaseData.Type;

            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type} found in vault.");
                return;
            }

            _items.Remove(type);
        }
    }
}