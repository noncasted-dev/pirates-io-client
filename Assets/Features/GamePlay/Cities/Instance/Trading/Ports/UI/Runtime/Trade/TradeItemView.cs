using System;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Slider _countSlider;
        [SerializeField] private TMP_Text _sliderValue;
        [SerializeField] private TMP_Text _cost;
        
        private TradableItem _item;
        private ItemOrigin _origin;
        private IPriceProvider _priceProvider;
        public TradableItem Item => _item;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(OnTransferClicked);
            _countSlider.onValueChanged.AddListener(OnSliderValueChanged);
            
            _countSlider.value = 1f;
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(OnTransferClicked);
            _countSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        public void AssignItem(TradableItem tradable, ItemOrigin origin, IPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
            _countSlider.value = 0;
            _sliderValue.text = 0.ToString();
            _origin = origin;
            _removeButton.gameObject.SetActive(true);

            _icon.sprite = tradable.Item.BaseData.Icon;
            _cost.text = tradable.Cost.ToString();

            _item = tradable;
            _countSlider.maxValue = tradable.Item.Count;
            gameObject.SetActive(true);

            UpdatePrice();
        }

        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        private int UpdatePrice()
        {
            if (_priceProvider == null)
                return 0;

            var count = (int)_countSlider.value;

            if (count == 0)
            {
                _countSlider.value = 1;
                count = 1;
            }

            var price = _origin switch
            {
                ItemOrigin.Cargo => _priceProvider.GetPlayerSellPrice(_item.Type, count),
                ItemOrigin.Stock => _priceProvider.GetStockSellPrice(_item.Type, count),
                _ => throw new ArgumentOutOfRangeException()
            };

            _cost.text = price.Median.ToString();
            _sliderValue.text = $"{count}";

            return price.Total;
        }

        private void OnSliderValueChanged(float value)
        {
            if (_item == null)
                return;
            
            var total = UpdatePrice();
            var count = (int)value;
            
            var tradeChange = new TradeAddedEvent(_item.Item, _origin, total, count);
            MessageBroker.Default.Publish(tradeChange);
        }

        private void OnTransferClicked()
        {
            _removeButton.gameObject.SetActive(false);

            var cancel = new TransferCanceledEvent(_item.Item, _origin);
            var removed = new TradeRemovedEvent(_item.Item, _origin);

            MessageBroker.Default.Publish(cancel);
            MessageBroker.Default.Publish(removed);
        }
    }
}