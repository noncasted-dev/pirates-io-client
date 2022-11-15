using UnityEngine;

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
        }

        public void Remove(int amount)
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
        }
    }
}