using System.Collections.Generic;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Events;
using UniRx;
using UnityEngine;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public class PlayerCargoStorage : MonoBehaviour, IPlayerCargoStorage
    {
        private readonly Dictionary<ItemType, IItem> _items = new();

        public IReadOnlyDictionary<ItemType, IItem> Items => _items;

        public void Add(IItem item)
        {
            var type = item.BaseData.Type;

            if (_items.ContainsKey(type) == true)
            {
                _items[type].Add(item.Count);
                OnChanged();
                return;
            }

            _items[type] = item;
            OnChanged();
        }

        public void Reduce(ItemType type, int amount)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items[type].Reduce(amount);
            
            OnChanged();

            if (_items[type].Count == 0)
                Delete(type);
        }

        public void Delete(ItemType type)
        {
            if (_items.ContainsKey(type) == false)
            {
                Debug.LogError($"No {type.ToString()} presented in player cargo");
                return;
            }

            _items.Remove(type);
            OnChanged();
        }

        public void Clear()
        {
            _items.Clear();
            OnChanged();
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
        
        public int GetWeight()
        {
            var weight = 0;

            foreach (var (_, item) in _items)
                weight += item.Count;

            return weight;
        }

        private void OnChanged()
        {
            var data = new CargoChangedEvent(Items, GetWeight());
            MessageBroker.Default.Publish(data);
        }
    }
}