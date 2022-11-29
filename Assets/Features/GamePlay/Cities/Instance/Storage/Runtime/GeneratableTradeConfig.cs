using System;
using GamePlay.Cities.Instance.Root.Runtime;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [Serializable]
    public class GeneratableTradeConfig
    {
        [SerializeField] [Min(1)] private int _medianCost;
        [SerializeField] [Min(1)] private int _medianMaxItems;

        [SerializeField] [Min(1)] private int _medianCount;
        [SerializeField] [Min(1)] private int _medianCurveHeight;
        
        [SerializeField] [Min(1)] private int _minCurveHeight;
        [SerializeField] [Min(1)] private int _minCount;
        [SerializeField] [Min(1)] private int _productionSpeed;
        
        [SerializeField] private CityType _city;

        public int MedianCost => _medianCost;
        public int MedianMaxItems => _medianMaxItems;
        public int MedianCount => _medianCount;
        public int MedianCurveHeight => _medianCurveHeight;
        public int MinCurveHeight => _minCurveHeight;
        public int MinCount => _minCount;
        public int ProductionSpeed => _productionSpeed;
        public CityType City => _city;
    }
}