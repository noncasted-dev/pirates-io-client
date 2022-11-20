using System.Collections;
using System.Collections.Generic;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Items.Abstract;
using Global.Services.ItemFactories.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public class CityStorage : SceneObject, IPriceProvider, ICityStorage
    {
        [Inject]
        private void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        private const float _waitTime = 0.5f;

        [SerializeField] private TradableItemDictionary _producables;
        [SerializeField] private ItemPriceCurvesConfigAsset _curves;
        [SerializeField] private float _commission = 0.5f;

        private readonly ItemsVault _vault = new();
        private readonly HashSet<ItemType> _freezed = new();

        private readonly WaitForSeconds _wait = new(_waitTime);

        private IItemFactory _itemFactory;

        public IReadOnlyDictionary<ItemType, IItem> Items => _vault.Items;

        protected override void OnAwake()
        {
            StartCoroutine(CalculateTradables());
        }

        public void Add(IItem item)
        {
            _vault.Add(item);
        }

        private IEnumerator CalculateTradables()
        {
            var progress = 0f;

            while (gameObject.activeSelf == true)
            {
                if (progress > ItemPriceCurvesConfigAsset.CurveTime)
                    progress = 0f;

                foreach (var (key, data) in _producables)
                {
                    if (_freezed.Contains(key) == true)
                        continue;

                    ProcessItem(key, data, progress);
                }

                progress += _waitTime;

                yield return _wait;
            }
        }

        private void ProcessItem(ItemType type, ItemPriceConfig config, float progress)
        {
            if (_vault.Items.ContainsKey(type) == false)
            {
                var newItem = _itemFactory.Create(type, 1);
                _vault.Add(newItem);

                return;
            }

            var item = _vault.Items[type];

            if (item.Count < config.MedianCount)
            {
                item.Add(config.LackProductionSpeedPerSecond);

                return;
            }

            if (item.Count > config.MedianCount)
            {
                var reduce = Mathf.Clamp(config.LackProductionSpeedPerSecond, 0, item.Count);
                item.Reduce(reduce);

                if (item.Count > config.MedianCount)
                    return;
            }

            var evaluation = _curves.ItemsInTime.Evaluate(progress);
            var additional = evaluation * config.CurveHeight;
            var amount = config.MedianCount + additional;

            item.SetCount((int)amount);
        }

        public void Freeze(ItemType type)
        {
            if (_freezed.Contains(type) == true)
                return;

            _freezed.Add(type);
        }

        public void Unfreeze(ItemType type)
        {
            if (_freezed.Contains(type) == false)
                return;

            _freezed.Remove(type);
        }

        public void UnfreezeAll()
        {
            _freezed.Clear();
        }

        public int GetPrice(ItemType type)
        {
            var count = _vault.Items[type].Count;
            var config = _producables[type];

            var progress = GetCountProgress(type, count);

            var priceEvaluation = _curves.ItemPricePerCount.Evaluate(progress);
            var cost = (int)(config.MedianCost * priceEvaluation);

            return cost;
        }

        private int GetPrice(ItemType type, int count)
        {
            count = _vault.Items[type].Count + count;
            var config = _producables[type];

            var progress = GetCountProgress(type, count);

            var priceEvaluation = _curves.ItemPricePerCount.Evaluate(progress);
            var cost = (int)(config.MedianCost * priceEvaluation);

            return cost;
        }

        public SellPrice GetPlayerSellPrice(ItemType type, int count)
        {
            var totalPrice = 0;

            for (var i = 0; i < count; i++)
            {
                var price = GetPrice(type, i);
                var progress = GetCountProgress(type, i);
                var priceEvaluation = _curves.SellPricePerCount.Evaluate(progress);
                totalPrice += (int)(priceEvaluation * price * (1f - _commission));
            }

            var median = totalPrice / count;
            
            return new SellPrice(median, totalPrice);
        }

        public SellPrice GetStockSellPrice(ItemType type, int count)
        {
            var totalPrice = 0;

            for (var i = 0; i < count; i++)
            {
                var price = GetPrice(type, -i);
                var progress = GetCountProgress(type, i);
                var priceEvaluation = _curves.StockPricePerCount.Evaluate(progress);
                totalPrice += (int)(priceEvaluation * price);
            }
            
            var median = totalPrice / count;

            return new SellPrice(median, totalPrice);
        }

        private SellPrice GetTargetSellPrice(ItemType type, int count, AnimationCurve curve, float commission)
        {
            var totalPrice = 0;
            var lastPrice = 0;

            for (var i = 0; i < count; i++)
            {
                var price = GetPrice(type, i);
                var progress = GetCountProgress(type, i);
                var priceEvaluation = curve.Evaluate(progress);
                lastPrice = (int)(priceEvaluation * price * (1f - commission));
                totalPrice += lastPrice;
            }

            return new SellPrice(lastPrice, totalPrice);
        }

        private float GetCountProgress(ItemType type, int count)
        {
            var config = _producables[type];

            var progress = count / (float)config.MaxItems;

            if (progress > 1f)
                progress = 1f;

            return progress;
        }
    }
}