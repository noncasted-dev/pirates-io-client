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
    public class TradeShipView : TradeView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _removeButton;
        [SerializeField] private TMP_Text _cost;
        
        private TradableItem _item;
        private ItemOrigin _origin;
        private IPriceProvider _priceProvider;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(OnTransferClicked);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(OnTransferClicked);
        }

        public override void AssignItem(TradableItem tradable, ItemOrigin origin, IPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
            _origin = origin;
            _removeButton.gameObject.SetActive(true);

            _icon.sprite = tradable.Item.BaseData.Icon;
            _cost.text = tradable.Cost.ToString();

            _item = tradable;
            gameObject.SetActive(true);

            var total = _priceProvider.GetStockSellPrice(_item.Type, 1);
            
            var tradeChange = new TradeAddedEvent(_item.Item, _origin, total.Total, 1);
            MessageBroker.Default.Publish(tradeChange);
        }

        public override void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
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