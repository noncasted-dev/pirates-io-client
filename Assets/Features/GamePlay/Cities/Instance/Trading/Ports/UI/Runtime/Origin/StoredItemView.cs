using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Items.Abstract;
using Global.Services.MessageBrokers.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin
{
    public class StoredItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _transferButton;

        private IItem _item;
        private ItemOrigin _origin;
        private bool _isActive;
        private int _price;

        private IPriceProvider _priceProvider;

        private void OnEnable()
        {
            _transferButton.onClick.AddListener(OnTransferClicked);
            
            if (_item == null)
                return;

            _item.CountChanged += OnCountChanged;
        }

        private void OnDisable()
        {
            _transferButton.onClick.RemoveListener(OnTransferClicked);
            
            if (_item == null)
                return;

            _item.CountChanged -= OnCountChanged;
        }

        public void AssignItem(IItem item, ItemOrigin origin, IPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
            _origin = origin;
            _item = item;
            _isActive = false;
            
            gameObject.SetActive(true);
            _transferButton.gameObject.SetActive(true);
            
            _icon.sprite = item.BaseData.Icon;
            _count.text = item.Count.ToString();
            _name.text = item.BaseData.Name;
            
            var price = _priceProvider.GetPrice(item.BaseData.Type);
            _cost.text = price.ToString();

            OnCountChanged(_item.Count);

        }

        public void OnTransferedItemCountChange(int count)
        {
            if (_item == null)
                return;
            
            var delta = _item.Count - count;

            _count.text = delta.ToString();

            if (delta == 0)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (_item != null && _isActive == true)
                _priceProvider.Unfreeze(_item.BaseData.Type);
            
            gameObject.SetActive(false);
            _item = null;

            _isActive = false;
        }

        public void OnTransferCanceled()
        {
            if (_item == null)
                return;
            
            gameObject.SetActive(true);
            _count.text = _item.Count.ToString();
            _priceProvider.Unfreeze(_item.BaseData.Type);

            _isActive = false;

            _transferButton.gameObject.SetActive(true);
        }

        public void UpdatePrice()
        {
            if (_item == null)
                return;

            if (_priceProvider == null)
                return;

            if (_isActive == true)
                return;

            _price = _priceProvider.GetPrice(_item.BaseData.Type);
            _cost.text = _price.ToString();
        }

        private void OnTransferClicked()
        {
            if (_item == null)
                return;
            
            _priceProvider.Freeze(_item.BaseData.Type);

            _isActive = true;
            _transferButton.gameObject.SetActive(false);

            var tradable = new TradableItem(_item, _price);

            var data = new TransferRequestedEvent(tradable, _origin);

            Msg.Publish(new TradeRequestedEvent());
            Msg.Publish(data);
        }

        private void OnCountChanged(int count)
        {
            _count.text = count.ToString();
        }
    }
}