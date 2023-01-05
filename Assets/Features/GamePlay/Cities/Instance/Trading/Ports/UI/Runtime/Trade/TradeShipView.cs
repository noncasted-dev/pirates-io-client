using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Player.Entity.Components.Definition;
using Global.Services.MessageBrokers.Runtime;
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
        
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _team;
        [SerializeField] private TMP_Text _cannons;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _name;
        
        private TradableItem _item;
        private ItemOrigin _origin;

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
            _origin = origin;
            _removeButton.gameObject.SetActive(true);

            _icon.sprite = tradable.Item.BaseData.Icon;
            _cost.text = tradable.Cost.ToString();

            _item = tradable;
            gameObject.SetActive(true);

            var tradeChange = new TradeAddedEvent(_item.Item, _origin, tradable.Cost, 1);
            Msg.Publish(tradeChange);

            var ship = tradable.Item as ShipItem;
            
            _health.text = ship.MaxHealth.ToString();
            _weight.text = ship.MaxWeight.ToString();
            _team.text = ship.MaxTeam.ToString();
            _cannons.text = ship.MaxCannons.ToString();
            _speed.text = ship.MaxSpeed.ToString();
            _name.text = ship.BaseData.Name;
            _cost.text = ship.Price.ToString();
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

            Msg.Publish(cancel);
            Msg.Publish(removed);
        }
    }
}