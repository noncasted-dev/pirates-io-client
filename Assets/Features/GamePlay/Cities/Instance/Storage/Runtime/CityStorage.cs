using System.Collections;
using System.Collections.Generic;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Items.Abstract;
using Global.Services.ItemFactories.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public class CityStorage : SceneObject, IPriceProvider
    {
        [Inject]
        private void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        private const float _waitTime = 0.5f;

        [SerializeField] private TradableItemDictionary _producables;
        [SerializeField] private ItemPriceCurvesConfigAsset _curves;

        private readonly ItemsVault _vault = new();
        private readonly HashSet<ItemType> _freezed = new();

        private readonly WaitForSeconds _wait = new(_waitTime);

        private IItemFactory _itemFactory;

        public IReadOnlyDictionary<ItemType, IItem> Items => _vault.Items;

        protected override void OnAwake()
        {
            StartCoroutine(CalculateTradables());
        }

        private void Add(IItem item)
        {
            _vault.Add(item);
        }

        private void Reduce(IItem item, int count)
        {
            _vault.Reduce(item, count);
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

            if (item.Count < config.MedianCost)
            {
                item.Add(config.LackProductionSpeedPerSecond);

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

        public int GetPrice(ItemType type)
        {
            var config = _producables[type];
            var item = _vault.Items[type];

            var progress = GetCountProgress(type, item.Count);

            var priceEvaluation = _curves.ItemPricePerCount.Evaluate(progress);
            var cost = (int)(config.MedianCost * priceEvaluation);

            return cost;
        }

        public int GetPlayerSellPrice(ItemType type, int count)
        {
            var price = GetPrice(type);
            var totalPrice = 0;

            for (var i = 0; i < count; i++)
            {
                var progress = GetCountProgress(type, count);
                var priceEvaluation = _curves.SellPricePerCount.Evaluate(progress);
                totalPrice += (int)(priceEvaluation * price);
            }

            return totalPrice;
        }

        public int GetStockSellPrice(ItemType type, int count)
        {
            var price = GetPrice(type);
            var totalPrice = 0;

            for (var i = 0; i < count; i++)
            {
                var progress = GetCountProgress(type, count);
                var priceEvaluation = _curves.StockPricePerCount.Evaluate(progress);
                totalPrice += (int)(priceEvaluation * price);
            }

            return totalPrice;
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