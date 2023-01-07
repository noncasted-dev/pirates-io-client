using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.MessageBrokers.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin
{
    public class StoredShipView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _team;
        [SerializeField] private TMP_Text _cannons;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _name;

        [SerializeField] private Button _transferButton;
        private bool _isAvailable = false;

        private ShipItem _item;
        private ItemOrigin _origin;

        private void OnEnable()
        {
            _transferButton.onClick.AddListener(OnTransferClicked);
        }

        private void OnDisable()
        {
            _transferButton.onClick.RemoveListener(OnTransferClicked);
        }

        public void AssignItem(
            IItem item,
            ItemOrigin origin,
            IReputation reputation)
        {
            if (item is not ShipItem ship)
            {
                Debug.LogError($"Assigned item is not a ship: {item.BaseData.Type} from {origin}");
                return;
            }

            _origin = origin;
            _item = ship;

            _isAvailable = true;

            gameObject.SetActive(true);

            if (_isAvailable == true)
                _transferButton.gameObject.SetActive(true);
            else
                _transferButton.gameObject.SetActive(false);

            _icon.sprite = item.BaseData.Icon;
            _cost.text = ship.Price.ToString();
            _health.text = ship.MaxHealth.ToString();
            _weight.text = ship.MaxWeight.ToString();
            _team.text = ship.MaxTeam.ToString();
            _cannons.text = ship.MaxCannons.ToString();
            _speed.text = ship.MaxSpeed.ToString();
            _name.text = ship.BaseData.Name;
        }

        public void Enable()
        {
            gameObject.SetActive(true);

            if (_isAvailable == true)
                _transferButton.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            _transferButton.gameObject.SetActive(false);
        }

        private void OnTransferClicked()
        {
            var tradable = new TradableItem(_item, _item.Price);

            var data = new TransferRequestedEvent(tradable, _origin);

            Msg.Publish(new TradeRequestedEvent());
            Msg.Publish(data);

            gameObject.SetActive(false);
        }
    }
}