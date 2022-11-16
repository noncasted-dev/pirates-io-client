#region

using System.Collections.Generic;
using GamePlay.Items.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public class PlayerCargoStorage : MonoBehaviour, IPlayerCargoStorage
    {
        private readonly Dictionary<ItemType, Item> _items = new();

        public void Add(Item item)
        {
            var type = item.BaseData.Type;

            if (_items.ContainsKey(type) == true)
            {
                _items[type].Add(item.Count);
                return;
            }

            _items[type] = item;
        }

        public void Reduce(ItemType type, int amount)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items[type].Reduce(amount);
        }

        public void Delete(ItemType type)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items.Remove(type);
        }

        public Item[] ToArray()
        {
            var items = new Item[_items.Count];

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