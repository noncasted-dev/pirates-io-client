using System;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    [DisallowMultipleComponent]
    public class TradeMoney : MonoBehaviour
    {
        [SerializeField] private Slider _countSlider;
        [SerializeField] private TMP_Text _sliderValue;
        [SerializeField] private TMP_Text _cost;

        private TradableItem _item;
        private ItemOrigin _origin;
        private int _price;
        private IPriceProvider _priceProvider;
        public TradableItem Item => _item;

        private void OnEnable()
        {
            _countSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _countSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        public void AssignItem(TradableItem tradable, ItemOrigin origin, IPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
            _countSlider.value = 0;
            _sliderValue.text = 0.ToString();
            _origin = origin;
            gameObject.SetActive(true);
            _cost.text = tradable.Cost.ToString();

            _item = tradable;
            _countSlider.maxValue = tradable.Item.Count;

            UpdatePrice();
        }

        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        private void UpdatePrice()
        {
            if (_priceProvider == null)
                return;

            var count = (int)_countSlider.value;

            if (count == 0)
            {
                _countSlider.value = 1;
                count = 1;
            }

            _price = _origin switch
            {
                ItemOrigin.Cargo => _priceProvider.GetPlayerSellPrice(_item.Type, count),
                ItemOrigin.Stock => _priceProvider.GetStockSellPrice(_item.Type, count),
                _ => throw new ArgumentOutOfRangeException()
            };

            _cost.text = _price.ToString();
        }

        private void OnSliderValueChanged(float value)
        {
            UpdatePrice();
            
            var cost = _price * (int)value;
            _sliderValue.text = $"{(int)value}";
            var tradeChange = new TradeAddedEvent(_item.Type, _origin, cost);
            MessageBroker.Default.Publish(tradeChange);
        }

        private void OnTransferClicked()
        {
            var cancel = new TransferCanceledEvent(_item.Item, _origin);
            var removed = new TradeRemovedEvent(_item.Type, _origin);

            MessageBroker.Default.Publish(cancel);
            MessageBroker.Default.Publish(removed);
        }
    }
}