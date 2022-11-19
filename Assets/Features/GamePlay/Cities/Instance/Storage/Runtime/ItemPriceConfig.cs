using System;
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

        public int MedianCost => _medianCost;
        public int CurveHeight => _curveHeight;
        public int MedianCount => _medianCount;
        public int MaxItems => _maxItems;
        public int LackProductionSpeedPerSecond => _lackProductionSpeedPerSecond;
    }
}