using GamePlay.Common.Paths;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    [CreateAssetMenu(fileName =GamePlayAssetsPaths.Config + "ItemsPriceCurves",
        menuName = GamePlayAssetsPaths.CityPort + "ItemsPriceCurves")]
    public class ItemPriceCurvesConfigAsset : ScriptableObject
    {
        public const float CurveTime = 10f;
        
        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _pricePerCount;
        
        [SerializeField] [CurveRange(0f, 0f, CurveTime, 1f)]
        private AnimationCurve _itemsInTime;

        public AnimationCurve PricePerCount => _pricePerCount;
        public AnimationCurve ItemsInTime => _itemsInTime;
    }
}