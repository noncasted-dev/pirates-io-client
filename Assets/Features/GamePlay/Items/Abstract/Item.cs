#region

using System;
using UnityEngine;

#endregion

namespace GamePlay.Items.Abstract
{
    public class Item : IItem
    {
        public Item(
            BaseItemData data,
            int count)
        {
            BaseData = data;
            _count = count;
        }

        private int _count;

        public event Action<int> CountChanged;

        public BaseItemData BaseData { get; }
        public int Count => _count;

        public void Add(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError($"Wrong item add: {amount}");
                return;
            }

            _count += amount;

            CountChanged?.Invoke(_count);
        }

        public void Reduce(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError($"Wrong item {BaseData.Name} remove: {amount}");
                return;
            }

            _count -= amount;

            if (_count < 0)
            {
                Debug.LogError($"Item {BaseData.Name} count should be greater than zero.");
                _count = 0;
            }

            CountChanged?.Invoke(_count);
        }
    }
}