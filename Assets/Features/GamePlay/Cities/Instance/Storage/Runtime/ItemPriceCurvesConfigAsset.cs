using GamePlay.Common.Paths;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.Config + "ItemsPriceCurves",
        menuName = GamePlayAssetsPaths.CityPort + "ItemsPriceCurves")]
    public class ItemPriceCurvesConfigAsset : ScriptableObject
    {
        public const float CurveTime = 10f;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _itemPricePerCount;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _sellPricePerCount;

        [SerializeField] [CurveRange(0f, 1f, 1f, 3f)]
        private AnimationCurve _stockPricePerCount;

        [SerializeField] [CurveRange(0f, 0f, CurveTime, 1f)]
        private AnimationCurve _itemsInTime;

        public AnimationCurve ItemPricePerCount => _itemPricePerCount;
        public AnimationCurve SellPricePerCount => _sellPricePerCount;
        public AnimationCurve StockPricePerCount => _stockPricePerCount;
        public AnimationCurve ItemsInTime => _itemsInTime;
    }
}