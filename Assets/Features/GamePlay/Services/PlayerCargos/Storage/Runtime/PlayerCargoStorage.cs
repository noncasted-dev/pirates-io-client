using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public class PlayerCargoStorage : MonoBehaviour, IPlayerCargoStorage
    {
        private readonly Dictionary<ItemType, IItem> _items = new();

        public IReadOnlyDictionary<ItemType, IItem> Items => _items;

        public event Action Changed;

        public void Add(IItem item)
        {
            var type = item.BaseData.Type;

            if (_items.ContainsKey(type) == true)
            {
                _items[type].Add(item.Count);
                Changed?.Invoke();
                return;
            }

            _items[type] = item;
            Changed?.Invoke();
        }

        public void Reduce(ItemType type, int amount)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items[type].Reduce(amount);
            Changed?.Invoke();
        }

        public void Delete(ItemType type)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items.Remove(type);
            Changed?.Invoke();
        }

        public IItem[] ToArray()
        {
            var items = new IItem[_items.Count];

            var counter = 0;

            foreach (var (_, value) in _items)
            {
                items[counter] = value;
                counter++;
            }

            return items;
        }
    }
}