using System.Collections.Generic;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public class CityStorage : MonoBehaviour
    {
        private readonly ItemsVault _vault = new();

        public IReadOnlyDictionary<ItemType, IItem> GetItems()
        {
            return _vault.Copy();
        }

        public void SetItems(IReadOnlyDictionary<ItemType, IItem> items)
        {
            _vault.Set(items);
        }
    }
}