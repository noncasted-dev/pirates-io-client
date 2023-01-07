using System;
using GamePlay.Cities.Instance.Root.Runtime;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [Serializable]
    public class GeneratableTradeConfig
    {
        [SerializeField] [Min(1)] private int _medianCount;
        [SerializeField] [Min(1)] private int _medianCost;
        [SerializeField] [Min(1)] private int _medianCurveHeight;

        [SerializeField] [Min(1)] private int _maxItems;

        [SerializeField] [Min(1)] private int _minCurveHeight;
        [SerializeField] [Min(1)] private int _minCount;
        [SerializeField] [Min(1)] private int _productionSpeed;

        [SerializeField] private CityType _city;

        public GeneratableTradeConfig(CityType city)
        {
            _city = city;

            _medianCost = 100;
            _maxItems = 300;
            _medianCount = 100;
            _medianCurveHeight = 60;
            _minCurveHeight = 10;
            _minCount = 10;
            _productionSpeed = 1;
        }

        public int MedianCost => _medianCost;
        public int MaxItems => _maxItems;
        public int MedianCount => _medianCount;
        public int MedianCurveHeight => _medianCurveHeight;
        public int MinCurveHeight => _minCurveHeight;
        public int MinCount => _minCount;
        public int ProductionSpeed => _productionSpeed;
        public CityType City => _city;
    }
}