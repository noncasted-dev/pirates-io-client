using System;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Items.Implementation
{
    public class ShipItem : IItem
    {
        public ShipItem(
            BaseItemData data,
            int count,
            int maxTeam,
            int maxWeight,
            int maxCannons,
            int maxHealth,
            int maxSpeed,
            int price)
        {
            MaxSpeed = maxSpeed;
            BaseData = data;
            MaxTeam = maxTeam;
            MaxCannons = maxCannons;
            MaxWeight = maxWeight;
            MaxHealth = maxHealth;
            MaxWeight = maxWeight;
            Price = price;
            _count = count;
        }

        private int _count;

        public event Action<int> CountChanged;

        public BaseItemData BaseData { get; }
        public int MaxTeam { get; }
        public int MaxWeight { get; }
        public int MaxCannons { get; }
        public int MaxHealth { get; }
        public int Price { get; }
        public int MaxSpeed { get; }

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

        public void SetCount(int amount)
        {
            _count = amount;

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
                Debug.LogError($"Item {BaseData.Name} count should be greater than zero: reduce: {amount}, result: {_count}.");
                _count = 0;
            }

            CountChanged?.Invoke(_count);
        }

        public IItem Copy()
        {
            return new Item(BaseData, _count);
        }
    }
}