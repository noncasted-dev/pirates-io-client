using System.Collections.Generic;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace Global.Services.PersistentInventories.Runtime
{
    public class PersistentInventory : MonoBehaviour, IPersistentInventory
    {
        private readonly Dictionary<CityType, StoredItemsDictionary> _storage = new();

        public void StoreItem(CityType city, StoredItem item)
        {
            if (_storage.ContainsKey(city) == false)
            {
                var newItems = new StoredItemsDictionary();
                _storage.Add(city, newItems);
            }

            var items = _storage[city];
            items[item.Type] = item.Amount;
        }

        public StoredItemsDictionary GetItems(CityType city)
        {
            if (_storage.ContainsKey(city) == false)
                return new StoredItemsDictionary();

            return _storage[city];
        }
    }
}