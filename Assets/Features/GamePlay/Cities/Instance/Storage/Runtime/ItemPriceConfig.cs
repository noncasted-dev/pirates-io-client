using System;
using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [Serializable]
    public class ItemPriceConfig
    {
        [SerializeField] [Min(1)] private int _medianCost = 1;
        [SerializeField] [Min(1)] private int _curveHeight = 100;
        [SerializeField] [Min(1)] private int _medianCount = 100;
        [SerializeField] [Min(1)] private int _maxItems = 100;
        [SerializeField] [Min(1)] private int _lackProductionSpeedPerSecond = 10;
        [SerializeField] private ItemType _type;

        public ItemPriceConfig(
            int cost,
            int height,
            int count,
            int max,
            int speed,
            ItemType type)
        {
            _medianCost = cost;
            _curveHeight = height;
            _medianCount = count;
            _maxItems = max;
            _lackProductionSpeedPerSecond = speed;
            _type = type;
        }

        public int MedianCost => _medianCost;
        public int CurveHeight => _curveHeight;
        public int MedianCount => _medianCount;
        public int MaxItems => _maxItems;
        public int LackProductionSpeedPerSecond => _lackProductionSpeedPerSecond;
        public ItemType Type => _type;
    }
}