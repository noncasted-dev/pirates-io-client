using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Definition;
using Global.Services.ItemFactories.Runtime;
using UnityEngine;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public class CityStorage : SceneObject, IPriceProvider, ICityStorage
    {
        public void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        private const float _waitTime = 1f;
        private const int _producablesConfigCount = 3;

        [SerializeField] private List<TradableShipConfig> _shipConfigs;
        [SerializeField] private TradableItemDictionary _producables;
        [SerializeField] private ItemPriceCurvesConfigAsset _curves;
        [SerializeField] private ItemType _debug;
        [SerializeField] private bool _isDebug;
        private readonly ItemsVault _vault = new();
        private readonly HashSet<ItemType> _freezed = new();
        private readonly Dictionary<ItemType, IItem> _ships = new();

        private readonly List<ItemType> _mostProduced = new();
        private readonly List<ItemType> _lessProduces = new();

        private readonly WaitForSeconds _wait = new(_waitTime);

        private IItemFactory _itemFactory;

        public IReadOnlyDictionary<ItemType, IItem> Items => _vault.Items;
        public IReadOnlyDictionary<ItemType, IItem> Ships => _ships;

        protected override void OnAwake()
        {
            foreach (var (key, data) in _producables)
            {
                if (_freezed.Contains(key) == true)
                    continue;

                var item = _itemFactory.Create(key, data.MedianCount);
                _vault.Add(item);
            }
            
            StartCoroutine(CalculateTradables());
        }

        public void Clear()
        {
            _producables.Clear();
        }

        public void AddProducable(ItemType type, ItemPriceConfig config)
        {
            _producables.Add(type, config);
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
                foreach (var (key, data) in _producables)
                {
                    if (_freezed.Contains(key) == true)
                        continue;

                    ProcessItem(key, data);
                }

                foreach (var ship in _shipConfigs)
                {
                    if (_ships.ContainsKey(ship.Type) == true)
                        continue;

                    _ships.Add(ship.Type, ship.Create(2));
                }

                yield return _wait;
            }
        }

        private void ProcessItem(ItemType type, ItemPriceConfig config)
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
                if (_isDebug == true)
                {
                    if (_debug == type)
                    {
                    }
                }
                
                item.Add(config.LackProductionSpeedPerSecond);

                return;
            }

            if (item.Count > config.MedianCount + config.MedianCount * (float)config.CurveHeight)
            {
                if (_isDebug == true)
                {
                    if (_debug == type)
                    {
                    }
                }
                
                var reduce = Mathf.Clamp(config.LackProductionSpeedPerSecond, 0, item.Count);
                item.Reduce(reduce);

                if (item.Count > config.MedianCount)
                    return;
            }

            var evaluation = _curves.ItemsInTime.Evaluate(Random.Range(0f, 1f));
            var additional = evaluation * config.CurveHeight;
            var amount = config.MedianCount + additional;
            
            if (_isDebug == true)
            {
                if (_debug == type)
                {
                }
            }

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
            var count = 1;

            if (_vault.Items.ContainsKey(type) == true)
                count = _vault.Items[type].Count;

            var config = _producables[type];

            var progress = GetCountProgress(type, count);

            var priceEvaluation = _curves.ItemPricePerCount.Evaluate(progress);

            var cost = (int)(config.MedianCost * priceEvaluation);

            if (cost < 1)
                return 1;

            return cost;
        }

        private int GetPrice(ItemType type, int count)
        {
            count = _vault.Items[type].Count + count;
            var config = _producables[type];

            var progress = GetCountProgress(type, count);

            var priceEvaluation = _curves.ItemPricePerCount.Evaluate(progress);
            var cost = (int)(config.MedianCost * priceEvaluation);

            if (cost < 1)
                return 1;
            
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
                var resultPrice = priceEvaluation * price * (1f - 0.05f);

                if (resultPrice < 1)
                    resultPrice = 1;
                
                totalPrice += (int)resultPrice;
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

                if (price < 1)
                    price = 1;
                
                var progress = GetCountProgress(type, i);
                var priceEvaluation = _curves.StockPricePerCount.Evaluate(progress);

                var resultPrice = priceEvaluation * price;
                
                if (resultPrice < 1)
                    resultPrice = 1;
                
                totalPrice += (int)resultPrice;
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
                
                if (price < 1)
                    price = 1;
                
                var progress = GetCountProgress(type, i);
                var priceEvaluation = curve.Evaluate(progress);
                lastPrice = Mathf.CeilToInt(priceEvaluation * price * (1f - commission));
                
                if (lastPrice < 1)
                    lastPrice = 1;
                
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